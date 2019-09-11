using AutoMapper;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<OrganizationViewModel, Organization>()
            .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()))
            .ForMember(x => x.AddedDate, opts => opts.Ignore())
            .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
            .ForMember(x => x.IsActive, opts => opts.Ignore());

            CreateMap<Organization, OrganizationViewModel>();
        }
    }
}
