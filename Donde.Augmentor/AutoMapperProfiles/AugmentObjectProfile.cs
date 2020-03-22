using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;
using Newtonsoft.Json;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AugmentObjectProfile : Profile
    {
        public AugmentObjectProfile()
        {
            CreateMap<AugmentObjectDto, AugmentObjectViewModel>()
               .ForMember(x => x.AvatarConfigurationString, opts => opts.MapFrom(src => src.AvatarConfiguration))
               .ForMember(x => x.AvatarConfiguration, opts => opts.Ignore());

            CreateMap<GeographicalAugmentObjectDto, GeographicalAugmentObjectsViewModel>()
              .ForMember(x => x.AvatarConfigurationString, opts => opts.MapFrom(src => src.AvatarConfiguration))
              .ForMember(x => x.AvatarConfiguration, opts => opts.Ignore());

            CreateMap<AugmentObjectPostViewModel, AugmentObject>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()))
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore())
                .ForMember(x => x.AugmentImage, opts => opts.Ignore())
                .ForMember(x => x.Organization, opts => opts.Ignore())
                .ForMember(x => x.AugmentObjectMedias, opts => opts.Ignore())
                .ForMember(x => x.AugmentObjectLocations, opts => opts.Ignore());

            // after post return
            CreateMap<AugmentObject, GeographicalAugmentObjectsViewModel>()
               .ForMember(x => x.MediaType, opts => opts.Ignore())
               .ForMember(x => x.Distance, opts => opts.Ignore())
               .ForMember(x => x.Latitude, opts => opts.Ignore())
               .ForMember(x => x.Longitude, opts => opts.Ignore())
               .ForMember(x => x.AvatarId, opts => opts.Ignore())
               .ForMember(x => x.AvatarName, opts => opts.Ignore())
               .ForMember(x => x.AvatarUrl, opts => opts.Ignore())
               .ForMember(x => x.AvatarConfiguration, opts => opts.Ignore())
                .ForMember(x => x.AvatarConfigurationString, opts => opts.Ignore())
               .ForMember(x => x.AudioId, opts => opts.Ignore())
               .ForMember(x => x.AudioName, opts => opts.Ignore())
               .ForMember(x => x.AudioUrl, opts => opts.Ignore())
               .ForMember(x => x.ImageName, opts => opts.Ignore())
               .ForMember(x => x.ImageUrl, opts => opts.Ignore())
               .ForMember(x => x.VideoId, opts => opts.Ignore())
               .ForMember(x => x.VideoName, opts => opts.Ignore())
               .ForMember(x => x.VideoUrl, opts => opts.Ignore());
        }
    }
}
