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
            .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()));

            CreateMap<AugmentObjectLocation, AugmentObjectLocationViewModel>();
        }
    }
}
