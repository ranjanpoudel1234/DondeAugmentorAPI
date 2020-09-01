using AutoMapper;
using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels.V2.Targets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.Target
{
    public class TargetProfile : Profile
    {
        public TargetProfile()
        {

            //Get
            CreateMap<AugmentObject, TargetViewModel>()
                 .ForMember(x => x.CreatedDateUtc, opts => opts.MapFrom(src => src.AddedDate))
                 .ForMember(x => x.UpdatedDateUtc, opts => opts.MapFrom(src => src.UpdatedDate))
                 .ForMember(x => x.Image, opts => opts.MapFrom(src => src.AugmentImage))
                 .ForMember(x => x.Media, opts => opts.MapFrom(src => src.AugmentObjectMedias))
                 .ForMember(x => x.Avatar, opts => opts.MapFrom(src => src.AugmentObjectMedias))
                 .ForMember(x => x.Locations, opts => opts.MapFrom(src => src.AugmentObjectLocations));

            CreateMap<AugmentImage, TargetImageViewModel>()
                .ForMember(x => x.Url, opts => opts.Ignore()) // these will mapped afterwards
                .ForMember(x => x.ThumbnailUrl, opts => opts.Ignore());

            CreateMap<List<AugmentObjectMedia>, TargetMediaViewModel>() // as of now, there is only one media. Its a list preparing for future.
                .ForMember(x => x.Type, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType)) // these will mapped afterwards
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.Id : src.FirstOrDefault().Video.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.Name : src.FirstOrDefault().Video.Name))
                 .ForMember(x => x.FileId, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.FileId : src.FirstOrDefault().Video.FileId))
                .ForMember(x => x.Extension, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.Extension : src.FirstOrDefault().Video.Extension))
                .ForMember(x => x.Url, opts => opts.Ignore());// these will mapped afterwards

            CreateMap<List<AugmentObjectMedia>, TargetAvatarViewModel>()
               .ForMember(x => x.Id, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.Id : Guid.Empty))
               .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.Name : null))
               .ForMember(x => x.FileId, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.FileId : Guid.Empty))
               .ForMember(x => x.OrganizationId, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.OrganizationId : Guid.Empty))
               .ForMember(x => x.Extension, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.Extension : null))
               .ForMember(x => x.Url, opts => opts.Ignore()) // these will mapped afterwards
               .ForMember(x => x.Configuration, opts => opts.Ignore()) // mapped in controller
               .ForMember(x => x.ConfigurationString, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.AvatarConfiguration : null));

            CreateMap<AugmentObjectLocation, TargetLocationViewModel>();


            //creation

            CreateMap<TargetViewModel, AugmentObject>()
                .ForMember(x => x.Id, opts => opts.Ignore()) //mapped in service
                 .ForMember(x => x.AugmentImageId, opts => opts.MapFrom(src => src.Image.Id))
                 .ForMember(x => x.AddedDate, opts => opts.Ignore())
                 .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                 .ForMember(x => x.IsDeleted, opts => opts.Ignore())
                 .ForMember(x => x.AugmentImage, opts => opts.Ignore())
                 .ForMember(x => x.Organization, opts => opts.Ignore())
                 .ForMember(x => x.AugmentObjectMedias, opts => opts.Ignore())
                 .ForMember(x => x.AugmentObjectLocations, opts => opts.Ignore());

            CreateMap<TargetMediaViewModel, AugmentObjectMedia>()
               .ForMember(x => x.Id, opts => opts.Ignore()) //mapped in service
               .ForMember(x => x.AvatarId, opts => opts.Ignore()) //mapped in its automapper below.
               .ForMember(x => x.AugmentObjectId, opts => opts.Ignore()) //mapped in service
               .ForMember(x => x.AudioId, opts => opts.MapFrom(src => src.Type == AugmentObjectMediaTypes.AvatarWithAudio ? src.Id : (Guid?)null))
               .ForMember(x => x.VideoId, opts => opts.MapFrom(src => src.Type == AugmentObjectMediaTypes.Video ? src.Id : (Guid?)null))
               .ForMember(x => x.MediaType, opts => opts.MapFrom(src => src.Type))
               .ForMember(x => x.AddedDate, opts => opts.Ignore())
               .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
               .ForMember(x => x.IsDeleted, opts => opts.Ignore())
               .ForMember(x => x.Audio, opts => opts.Ignore())
               .ForMember(x => x.Video, opts => opts.Ignore())
               .ForMember(x => x.Avatar, opts => opts.Ignore())
               .ForMember(x => x.AugmentObject, opts => opts.Ignore());


            CreateMap<TargetAvatarViewModel, AugmentObjectMedia>()
               .ForMember(x => x.AvatarId, opts => opts.MapFrom(src => src != null ? src.Id : (Guid?) null))
               .ForMember(x => x.Id, opts => opts.Ignore()) //mapped in service
               .ForMember(x => x.AvatarId, opts => opts.Ignore()) //mapped in its automapper below.
               .ForMember(x => x.AugmentObjectId, opts => opts.Ignore()) //mapped in service
               .ForMember(x => x.AudioId, opts => opts.Ignore())
               .ForMember(x => x.VideoId, opts => opts.Ignore())
               .ForMember(x => x.MediaType, opts => opts.Ignore())
               .ForMember(x => x.AddedDate, opts => opts.Ignore())
               .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
               .ForMember(x => x.IsDeleted, opts => opts.Ignore())
               .ForMember(x => x.Audio, opts => opts.Ignore())
               .ForMember(x => x.Video, opts => opts.Ignore())
               .ForMember(x => x.Avatar, opts => opts.Ignore())
               .ForMember(x => x.AugmentObject, opts => opts.Ignore());

            CreateMap<TargetLocationViewModel, AugmentObjectLocation>()
              .ForMember(x => x.Id, opts => opts.Ignore()) //mapped in service
              .ForMember(x => x.AugmentObjectId, opts => opts.Ignore()) //mapped in service      
              .ForMember(x => x.AddedDate, opts => opts.Ignore())
              .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
              .ForMember(x => x.IsDeleted, opts => opts.Ignore())       
              .ForMember(x => x.AugmentObject, opts => opts.Ignore());
        }
    }
}
