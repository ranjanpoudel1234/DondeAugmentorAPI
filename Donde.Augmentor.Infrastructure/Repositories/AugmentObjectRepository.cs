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

        public IQueryable<AugmentObject> GetAugmentObjectsQueryableWithChildren()
        {
            return GetAugmentObjectWithChildrenQueryable();   
        }

        public Task<AugmentObject> GetAugmentObjectByIdWithChildrenAsync(Guid id)
        {
            return GetAugmentObjectWithChildrenQueryable()
                .SingleOrDefaultAsync(x => x.Id == id);
        }
   
        public Task<AugmentObject> GetAugmentObjectByIdithChildrenAsNoTrackingAsync(Guid id)
        {
            return GetAugmentObjectWithChildrenQueryable().AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        private IQueryable<AugmentObject> GetAugmentObjectWithChildrenQueryable()
        {
            return _dbContext.AugmentObjects
               .Include(au => au.AugmentImage)
               .Include(au => au.AugmentObjectMedias)
                   .ThenInclude(aum => aum.Audio)
               .Include(au => au.AugmentObjectMedias)
                   .ThenInclude(aum => aum.Video)
               .Include(au => au.AugmentObjectMedias)
                   .ThenInclude(aum => aum.Avatar)
               .Include(au => au.AugmentObjectLocations)
               .Include(au => au.Organization);
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
                                 join location in _dbContext.AugmentObjectLocations on augmentObject.Id equals location.AugmentObjectId into augmentObjectLocation
                                 from location in augmentObjectLocation.DefaultIfEmpty()
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
                                     MediaId = augmentObjectMedia.Id,
                                     ImageName = augmentImage.Name,
                                     ImageFileExtension = augmentImage.Extension,
                                     ImageFileId = augmentImage.FileId,
                                     AvatarId = augmentObjectMedia.AvatarId,
                                     AvatarName = avatar == null ? null: avatar.Name,
                                     AvatarConfiguration = avatar == null ? null : avatar.AvatarConfiguration,
                                     AvatarFileExtension = avatar == null ? null : avatar.Extension,
                                     AvatarFileId = avatar == null ? (Guid?)null : avatar.FileId,
                                     AudioId = augmentObjectMedia.AudioId,
                                     AudioName = audio == null ? null : audio.Name,
                                     AudioFileExtension = audio == null ? null : audio.Extension,
                                     AudioFileId = audio == null ? (Guid?)null : audio.FileId,
                                     VideoId = augmentObjectMedia.VideoId,
                                     VideoName = video == null ? null : video.Name,
                                     VideoFileExtension = video == null ? null : video.Extension,
                                     VideoFileId = video == null ? (Guid?)null : video.FileId,
                                     Latitude = location.Latitude,
                                     Longitude = location.Longitude
                                 };

            return augmentObjects;
        }

        //todo check if filter applies here for isDeletion
        //Also update the  fact that URL has been removed now. Needs update and testing
        public async Task<IEnumerable<AugmentObjectDto>> GetGeographicalAugmentObjectsByRadius(Guid organizationId, double latitude, double longitude, int radiusInMeters)
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
                        aoMedia.""Id"" as MediaId,
                        ai.""Name"" as ImageName,
                        ai.""Extension"" as ImageFileExtension,
                        ai.""FileId"" as ImageFileId,
                        aoMedia.""AvatarId"" as AvatarId,
                        av.""Name"" as AvatarName,
                        av.""Extension"" as AvatarFileExtension,
                        av.""FileId"" as AvatarFileId,
                        av.""AvatarConfiguration"" as AvatarConfiguration,
                        aoMedia.""AudioId"" as AudioId,
                        au.""Name"" as AudioName,
                        au.""Extension"" as AudioFileExtension,
                        au.""FileId"" as AudioFileId,
                        aoMedia.""VideoId"" as VideoId,
                        v.""Name"" as VideoName,
                        v.""Extension"" as VideoFileExtension,
                        v.""FileId"" as VideoFileId,
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
 where d.""IsDeleted"" = false
and d.""Type"" = {(int) AugmentObjectTypes.Geographical} 
and d.Distance < @RadiusInMeters 
and d.""OrganizationId"" = @OrganizationId
 order by d.Distance";
                 
            var connection = _dbContext.Database.GetDbConnection();

            var result = await connection.QueryAsync<AugmentObjectDto>
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
        //                        aO."IsDeleted",
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
        // where  where d.IsDeleted = false and d.AugmentObjectType = 1 and d.Distance< 5000 and d."OrganizationId" = 'c60054bb-53b3-4d26-98af-aac001399956' 
        // order by d.Distance

        //CREATE EXTENSION postgis; 
    }
}
