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
    public class AugmentObjectRepository : GenericRepository, IAugmentObjectRepository
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
 
        public async Task<IEnumerable<AugmentObjectDto>> GetClosestAugmentObjectsByRadius(double latitude, double longitude, int radiusInMeters)
        {
            string objectsByDistanceQuery = $@"with AugmentObjectWithDistance as 

                    (
                        SELECT 
                        st_distance(ST_Transform(CONCAT('SRID=4326;POINT(',""Longitude"",' ', ""Latitude"",')')::geometry. 3857), ST_Transform('SRID=4326;POINT(@longitude @latitude)':: geometry, 3857)) as Distance,
                        ""Id"",
                        ""AvatarId"",
                        ""AugmentImageId"",
                        ""Description"",
                        ""Latitude"",
                        ""Longitude"",
                        ""OrganizationId"",
                        ""AddedDate"",
                        ""UpdatedDate"",
                        ""IsActive""
                        from ""AugmentObjects""
                    )

                    SELECT 
                        *
                    FROM 
                        AugmentObjectWithDistance
                    WHERE
                        Distance < @radiusInMeters
                    ORDER BY
                        Distance";
                 
            var connection = _dbContext.Database.GetDbConnection();

            var result = await connection.QueryAsync<AugmentObjectDto>
            (
                objectsByDistanceQuery,
                new
                {
                    longitude,
                    latitude,
                    radiusInMeters
                }
            );

            return result;
        }
//; with AugmentObjectWithDistance as
//    (

//        select st_distance(ST_Transform(CONCAT('SRID=4326;POINT(',"Longitude",' ', "Latitude",')')::geometry, 3857), ST_Transform('SRID=4326;POINT(90.1009 30.4755)':: geometry, 3857)) as Distance,
//    	"Id",
//    	"AvatarId",
//    	 "AugmentImageId",
//                            "Description",
//                            "Latitude",
//                            "Longitude",
//                            "OrganizationId",
//                            "AddedDate",
//                            "UpdatedDate",
//                            "IsActive"
//    	from "AugmentObjects"
//    )
    
//    select* from AugmentObjectWithDistance
//   where Distance< 160000

//   order by Distance

        //CREATE EXTENSION postgis; 
    }
}
