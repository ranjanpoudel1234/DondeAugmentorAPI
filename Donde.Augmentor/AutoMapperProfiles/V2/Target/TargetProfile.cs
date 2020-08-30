using AutoMapper;
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
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.Id : src.FirstOrDefault().Video.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.Name : src.FirstOrDefault().Video.Name))
                 .ForMember(x => x.FileId, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.FileId : src.FirstOrDefault().Video.FileId))
                .ForMember(x => x.Extension, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.Extension : src.FirstOrDefault().Video.Extension))
                .ForMember(x => x.Url, opts => opts.Ignore());// these will mapped afterwards

            CreateMap<List<AugmentObjectMedia>, TargetAvatarViewModel>()
               .ForMember(x => x.Id, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.Id : Guid.Empty))
               .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.Name : null))
               .ForMember(x => x.FileId, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.FileId : Guid.Empty))
               .ForMember(x => x.OrganizationId, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.OrganizationId : Guid.Empty))
               .ForMember(x => x.Extension, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.Extension : null))
               .ForMember(x => x.Url, opts => opts.Ignore()) // these will mapped afterwards
               .ForMember(x => x.Configuration, opts => opts.Ignore()) // mapped in controller
               .ForMember(x => x.ConfigurationString, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Avatar.AvatarConfiguration : null));

            CreateMap<AugmentObjectLocation, TargetLocationViewModel>();
        }
    }
}
