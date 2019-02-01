using Dapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class AugmentObjectRepository : GenericRepository<AugmentObject>, IAugmentObjectRepository
    {
        public AugmentObjectRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public async Task<AugmentObject> CreateAugmentObjectAsync(AugmentObject entity)
        {
            return await CreateAsync(entity);
        }

        public async Task<AugmentObject> UpdateAugmentObjectAsync(Guid id, AugmentObject entity)
        {
            return await UpdateAsync(id, entity);
        }

       
        public async Task<IEnumerable<AugmentObjectDto>> GetAugmentObjectByImageId(double latitude, double longitude)
        {
            string objectsByDistanceQuery = $@"SELECT 
                            st_distance(CONCAT('SRID=4326;POINT(',""Latitude"",' ', ""Longitude"",')')::geometry, 'SRID=4326;POINT(@latitude @longitude)') as Distance,
                            Id,
                            AvatarId,
                            AugmentImageId,
                            Description,
                            Latitude,
                            Longitude,
                            OrganizationId,
                            AddedDate,
                            UpdatedDate,
                            IsActive
                            from ""AugmentObjects""
                            order by st_distance(CONCAT('SRID=4326;POINT(',""Latitude"",' ', ""Longitude"",')')::geometry, 'SRID=4326;POINT(@latitude @longitude)')";


            var connection = _dbContext.Database.GetDbConnection();

            var result = await connection.QueryAsync<AugmentObjectDto>
            (
                objectsByDistanceQuery,
                new
                {
                    latitude,
                    longitude
                }
            );

            return result;
        }

                //        select
                //            st_distance(CONCAT('SRID=4326;POINT(',"Latitude",' ', "Longitude",')')::geometry, 'SRID=4326;POINT(30.4755 90.1009)') as distance
                //            from public."AugmentObjects"
                //    order by st_distance(CONCAT('SRID=4326;POINT(',"Latitude",' ', "Longitude",')')::geometry, 'SRID=4326;POINT(30.4755 90.1009)') 

                //CREATE EXTENSION postgis; 
    }
}
