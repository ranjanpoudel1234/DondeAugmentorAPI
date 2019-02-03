using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Web.ViewModels;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AugmentObjectProfile : Profile
    {
        public AugmentObjectProfile()
        {
            CreateMap<AugmentObjectDto, AugmentObjectViewModel>();
        }
    }
}
