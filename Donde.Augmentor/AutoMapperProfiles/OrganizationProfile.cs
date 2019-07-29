using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationViewModel>();
        }
    }
}
