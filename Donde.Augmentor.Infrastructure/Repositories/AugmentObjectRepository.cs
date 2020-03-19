using Dapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Enum;
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
            // todo potentially make a readModel out of this for faster and efficient load.
            // also might look into dapper for it too.
            //OR
            //There is local warning on DefaultIfEmpty, hence we can load augmentObjects first with mediaType and Image, apply odata,
            // then call audio and video based on the mediatype and load those properties afterwards.THIS WILL MAKE IT BETTER TOO IMO THEN HAVING TO DO ALL THOSE JOINGs
            // BUT POST MVP
            var augmentObjects = from augmentObject in _dbContext.AugmentObjects
                                 join augmentObjectMedia in _dbContext.AugmentObjectMedias on augmentObject.Id equals augmentObjectMedia.AugmentObjectId
                                 join augmentImage in _dbContext.AugmentImages on augmentObject.AugmentImageId equals augmentImage.Id 
                                 join audio in _dbContext.Audios on augmentObjectMedia.AudioId equals audio.Id into augmentObjectAudio
                                 from audio in augmentObjectAudio.DefaultIfEmpty()
                                 join video in _dbContext.Videos on augmentObjectMedia.VideoId equals video.Id into augmentObjectVideo                          
                                 from video in augmentObjectVideo.DefaultIfEmpty()
                                 join avatar in _dbContext.Avatars on augmentObjectMedia.AvatarId equals avatar.Id into augmentObjectAvatar
                                 from avatar in augmentObjectAvatar.DefaultIfEmpty()
                                 select new AugmentObjectDto()
                                 {
                                     Id = augmentObject.Id,
                                     AugmentImageId = augmentObject.AugmentImageId,
                                     Title = augmentObject.Title,
                                     Description = augmentObject.Description,
                                     OrganizationId = augmentObject.OrganizationId,
                                     AddedDate = augmentObject.AddedDate,
                                     UpdatedDate = augmentObject.UpdatedDate,
                                     IsDeleted = augmentObject.IsDeleted,
                                     Type = augmentObject.Type,
                                     MediaType = augmentObjectMedia.MediaType,
                                     ImageName = augmentImage.Name,
                                     ImageUrl = augmentImage.Url,
                                     AvatarId = augmentObjectMedia.AvatarId,
                                     AvatarName = avatar == null ? null: avatar.Name,
                                     AvatarUrl = avatar == null? null: avatar.Url,
                                     AvatarConfiguration = avatar == null ? null : avatar.AvatarConfiguration,
                                     AudioId = augmentObjectMedia.AudioId,
                                     AudioName = audio == null ? null : audio.Name,
                                     AudioUrl = audio == null ? null : audio.Url,
                                     VideoId = augmentObjectMedia.VideoId,
                                     VideoName = video == null ? null : video.Name,
                                     VideoUrl = video == null ? null : video.Url                                                                                          
                                 };

            return augmentObjects;
        }

        //todo check if filter applies here for isDeletion
        public async Task<IEnumerable<GeographicalAugmentObjectDto>> GetGeographicalAugmentObjectsByRadius(Guid organizationId, double latitude, double longitude, int radiusInMeters)
        {
            string objectsByDistanceQuery = $@"with AugmentObjectWithDistance as (
 SELECT 
                     st_distance(ST_Transform(CONCAT('SRID=4326;POINT(',aoLocation.""Longitude"",' ', aoLocation.""Latitude"",')')::geometry, 3857), ST_Transform('SRID=4326;POINT({longitude} {latitude})':: geometry, 3857)) as Distance,
                        aO.""Id"",                     
                        aO.""AugmentImageId"",
                        aO.""Type"",
                        aO.""Title"",
                        aO.""Description"",                   
                        aO.""OrganizationId"",
                        aO.""AddedDate"",
                        aO.""UpdatedDate"",
                        aO.""IsDeleted"",
                        aoMedia.""MediaType"" as MediaType,
                        ai.""Name"" as ImageName,
                        ai.""Url"" as ImageUrl,
                        aoMedia.""AvatarId"" as AvatarId,
                        av.""Name"" as AvatarName,
                        av.""Url"" as AvatarUrl,
                        av.""AvatarConfiguration"" as AvatarConfiguration,
                        aoMedia.""AudioId"" as AudioId,
                        au.""Name"" as AudioName,
                        au.""Url"" as AudioUrl,
                        aoMedia.""VideoId"" as VideoId,
                        v.""Name"" as VideoName,
                        v.""Url"" as VideoUrl,
                        aoLocation.""Latitude"" as Latitude,
                        aoLocation.""Longitude"" as Longitude
                        from ""AugmentObjects"" ao
                        join
                            ""AugmentObjectLocations"" aoLocation on aoLocation.""AugmentObjectId"" = ao.""Id""
                        join
                            ""AugmentImages"" ai on ai.""Id"" = ao.""AugmentImageId""
                        join
                            ""AugmentObjectMedias"" aoMedia on aoMedia.""AugmentObjectId"" = ao.""Id""
                        left join
                            ""Audios"" au on au.""Id"" = aoMedia.""AudioId""
                        left join
                            ""Avatars"" av on av.""Id"" = aoMedia.""AvatarId""
                        left join
                            ""Videos"" v on v.""Id"" = aomedia.""VideoId""
)
select* from AugmentObjectWithDistance d
 where d.""Type"" = {(int) AugmentObjectTypes.Geographical} 
and d.Distance < @RadiusInMeters and d.""OrganizationId"" = @OrganizationId
 order by d.Distance";
                 
            var connection = _dbContext.Database.GetDbConnection();

            var result = await connection.QueryAsync<GeographicalAugmentObjectDto>
            (
                objectsByDistanceQuery,
                new
                {
                    Longitude = longitude,
                    Latitude = latitude,
                    RadiusInMeters = radiusInMeters,
                    OrganizationId = organizationId
                }
            );

            return result;
        }
//        with AugmentObjectWithDistance as (
//         SELECT
//                             st_distance(ST_Transform(CONCAT('SRID=4326;POINT(',aoLocation."Longitude",' ', aoLocation."Latitude",')')::geometry, 3857), ST_Transform('SRID=4326;POINT(-90.054006 30.459780)':: geometry, 3857)) as Distance,
//                        aO."Id",                     
//                        aO."AugmentImageId",
//                        aO."Type" as AugmentObjectType,
//                        aO."Title",
//                        aO."Description",                   
//                        aO."OrganizationId",
//                        aO."AddedDate",
//                        aO."UpdatedDate",
//                        aO."IsActive",
//                        aoMedia."MediaType" as MediaType,
//                        ai."Name" as ImageName,
//                        ai."Url" as ImageUrl,
//                        aoMedia."AvatarId",
//                        av."Name" as AvatarName,
//                        av."Url" as AvatarUrl,
//                        aoMedia."AudioId",
//                        au."Name" as "AudioName",
//                        au."Url" as "AudioUrl",
//                        aoMedia."VideoId",
//                        v."Name" as "VideoName",
//                        v."Url" as "VideoUrl",
//                        aoLocation."Latitude" as "Latitude",
//                        aoLocation."Longitude" as "Longitude"
                     
                                        
//                        from "AugmentObjects" ao
//                        join
//                        	"AugmentObjectLocations" aoLocation on aoLocation."AugmentObjectId" = ao."Id"
//                        join 
//                        	"AugmentImages" ai on ai."Id" = ao."AugmentImageId"
//                        join
//                        	"AugmentObjectMedias" aoMedia on aoMedia."AugmentObjectId" = ao."Id"
//                        left join
//                        	"Audios" au on au."Id" = aoMedia."AudioId"
//                        left join
//                        	"Avatars" av on av."Id" = aoMedia."AvatarId"
//                        left join
//                        	"Videos" v on v."Id" = aomedia."VideoId"        
//)
//select* from AugmentObjectWithDistance d
// where d.AugmentObjectType = 1 and d.Distance< 5000 and d."OrganizationId" = 'c60054bb-53b3-4d26-98af-aac001399956' 
// order by d.Distance

        //CREATE EXTENSION postgis; 
    }
}
