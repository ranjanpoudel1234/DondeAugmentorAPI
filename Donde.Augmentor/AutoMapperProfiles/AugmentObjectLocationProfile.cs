using AutoMapper;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels.AugmentObject;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AugmentObjectLocationProfile : Profile
    {
        public AugmentObjectLocationProfile()
        {
            CreateMap<AugmentObjectLocationViewModel, AugmentObjectLocation>()
            .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()))
            .ForMember(x => x.AddedDate, opts => opts.Ignore())
            .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
            .ForMember(x => x.IsDeleted, opts => opts.Ignore())
            .ForMember(x => x.AugmentObject, opts => opts.Ignore());

            CreateMap<AugmentObjectLocation, AugmentObjectLocationViewModel>();
        }
    }
}
