using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AugmentObjectProfile : Profile
    {
        public AugmentObjectProfile()
        {
            CreateMap<AugmentObjectDto, AugmentObjectViewModel>();

            CreateMap<AugmentObjectViewModel, AugmentObject>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()))
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsActive, opts => opts.Ignore());

            CreateMap<AugmentObject, AugmentObjectViewModel>()
                .ForMember(x => x.Distance, opts => opts.Ignore())
                .ForMember(x => x.AudioName, opts => opts.Ignore())
                .ForMember(x => x.AudioUrl, opts => opts.Ignore())
                .ForMember(x => x.ImageName, opts => opts.Ignore())
                .ForMember(x => x.ImageUrl, opts => opts.Ignore())
                .ForMember(x => x.VideoName, opts => opts.Ignore())
                .ForMember(x => x.VideoUrl, opts => opts.Ignore());
        }
    }
}
