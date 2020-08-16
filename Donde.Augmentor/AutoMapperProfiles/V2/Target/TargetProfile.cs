using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels.V2.Targets;
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
                 .ForMember(x => x.Avatar, opts => opts.MapFrom(src => src.Organization.Avatar))
                 .ForMember(x => x.Locations, opts => opts.MapFrom(src => src.AugmentObjectLocations));

            CreateMap<AugmentImage, TargetImageViewModel>()
                .ForMember(x => x.Url, opts => opts.Ignore()) // these will mapped afterwards
                .ForMember(x => x.ThumbnailUrl, opts => opts.Ignore());

            CreateMap<List<AugmentObjectMedia>, TargetMediaViewModel>()
                .ForMember(x => x.Type, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType)) // these will mapped afterwards
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio? src.FirstOrDefault().Audio.Id: src.FirstOrDefault().Video.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FirstOrDefault().MediaType == Core.Domain.Enum.AugmentObjectMediaTypes.AvatarWithAudio ? src.FirstOrDefault().Audio.Name : src.FirstOrDefault().Video.Name))
                .ForMember(x => x.Url, opts => opts.Ignore());// these will mapped afterwards

            CreateMap<Avatar, TargetAvatarViewModel>()
                .ForMember(x => x.Url, opts => opts.Ignore()) // these will mapped afterwards
                .ForMember(x => x.Configuration, opts => opts.Ignore()) // mapped in controller
                .ForMember(x => x.ConfigurationString, opts => opts.MapFrom(src => src.AvatarConfiguration));

            CreateMap<AugmentObjectLocation, TargetLocationViewModel>();
        }
    }
}
