using Dapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<AugmentObjectDto> GetAugmentObjects()
        {
            var augmentObjects = from augmentObject in _dbContext.AugmentObjects
                                 join augmentImage in _dbContext.AugmentImages on augmentObject.AugmentImageId equals augmentImage.Id 
                                 join audio in _dbContext.Audios on augmentObject.AudioId equals audio.Id into augmentObjectAudio
                                 from audio in augmentObjectAudio.DefaultIfEmpty()
                                 join video in _dbContext.Videos on augmentObject.VideoId equals video.Id into augmentObjectVideo                          
                                 from video in augmentObjectVideo.DefaultIfEmpty()
                                 select new AugmentObjectDto()
                                 {
                                     Id = augmentObject.Id,
                                     AvatarId = augmentObject.AvatarId,
                                     AudioId = augmentObject.AudioId,
                                     VideoId = augmentObject.VideoId,
                                     AugmentImageId = augmentObject.AugmentImageId,
                                     Title = augmentObject.Title,
                                     Description = augmentObject.Description,
                                     Latitude = augmentObject.Latitude,
                                     Longitude = augmentObject.Longitude,
                                     OrganizationId = augmentObject.OrganizationId,
                                     AddedDate = augmentObject.AddedDate,
                                     UpdatedDate = augmentObject.UpdatedDate,
                                     IsActive = augmentObject.IsActive,
                                     ImageName = augmentImage.Name,
                                     ImageUrl = augmentImage.Url,
                                     AudioName = audio == null ? null : audio.Name,
                                     AudioUrl = audio == null ? null : audio.Url,
                                     VideoName = video == null ? null : video.Name,
                                     VideoUrl = video == null ? null : video.Url
                                 };

            return augmentObjects;
        }

        public async Task<IEnumerable<AugmentObjectDto>> GetClosestAugmentObjectsByRadius(double latitude, double longitude, int radiusInMeters)
        {
            string objectsByDistanceQuery = $@"with AugmentObjectWithDistance as 

                    (
                        SELECT 
                        st_distance(ST_Transform(CONCAT('SRID=4326;POINT(',""Longitude"",' ', ""Latitude"",')')::geometry, 3857), ST_Transform('SRID=4326;POINT({longitude} {latitude})':: geometry, 3857)) as Distance,
                        aO.""Id"",
                        aO.""AvatarId"",
                        aO.""AudioId"",
                        aO.""AugmentImageId"",
                        aO.""Title"",
                        aO.""Description"",
                        aO.""Latitude"",
                        aO.""Longitude"",
                        aO.""OrganizationId"",
                        aO.""AddedDate"",
                        aO.""UpdatedDate"",
                        aO.""IsActive"",
                        ai.""Name"" as ImageName,
                        ai.""Url"" as ImageUrl,
                        au.""Name"" as AudioName,
                        au.""Url"" as AudioUrl
                                        
                        from ""AugmentObjects"" ao



                            join ""AugmentImages"" ai on ai.""Id"" = ao.""AugmentImageId""
                            join ""Audios"" au on au.""Id"" = ao.""AudioId""
                    )

                    SELECT 
                        *
                    FROM 
                        AugmentObjectWithDistance
                    WHERE
                        Distance < @RadiusInMeters
                    ORDER BY
                        Distance";
                 
            var connection = _dbContext.Database.GetDbConnection();

            var result = await connection.QueryAsync<AugmentObjectDto>
            (
                objectsByDistanceQuery,
                new
                {
                    Longitude = longitude,
                    Latitude = latitude,
                    RadiusInMeters = radiusInMeters
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
