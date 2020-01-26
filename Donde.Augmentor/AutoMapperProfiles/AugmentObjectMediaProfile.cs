using AutoMapper;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels.AugmentObject;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AugmentObjectMediaProfile : Profile
    {
        public AugmentObjectMediaProfile()
        {
            CreateMap<AugmentObjectMediaViewModel, AugmentObjectMedia>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()));

            CreateMap<AugmentObjectMedia, AugmentObjectMediaViewModel>();
        }
    }
}
