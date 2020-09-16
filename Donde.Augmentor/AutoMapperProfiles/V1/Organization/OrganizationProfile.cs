using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels.V1;
using System.Linq;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationViewModel>()
                .ForMember(x => x.Latitude, opts => opts.MapFrom(src => src.Sites.FirstOrDefault(x => x.Type == Core.Domain.Enum.SiteTypes.Main).Latitude))
                .ForMember(x => x.Longitude, opts => opts.MapFrom(src => src.Sites.FirstOrDefault(x => x.Type == Core.Domain.Enum.SiteTypes.Main).Longitude))
                .ForMember(x => x.Code, opts => opts.MapFrom(src => src.ShortName))
                .ForMember(x => x.LogoUrl, opts => opts.Ignore())
                .ForMember(x => x.Address, opts => opts.MapFrom(src => $"{src.StreetAddress1}, {src.City}, {src.State} {src.Zip}"));
        }
    }
}
