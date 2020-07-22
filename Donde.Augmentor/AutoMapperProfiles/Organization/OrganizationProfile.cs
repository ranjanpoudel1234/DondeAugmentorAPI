using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;
using System.Linq;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<MediaAttachmentDto, Organization>()
                .ForMember(x => x.LogoName, opts => opts.MapFrom(src => src.FileName))
                .ForMember(x => x.LogoUrl, opts => opts.MapFrom(src => src.FilePath))
                .ForMember(x => x.LogoMimeType, opts => opts.MapFrom(src => src.MimeType))
                .ForMember(x => x.Name, opts => opts.Ignore())
                .ForMember(x => x.ShortName, opts => opts.Ignore())
                .ForMember(x => x.StreetAddress1, opts => opts.Ignore())
                .ForMember(x => x.StreetAddress2, opts => opts.Ignore())
                .ForMember(x => x.City, opts => opts.Ignore())
                .ForMember(x => x.State, opts => opts.Ignore())
                .ForMember(x => x.Zip, opts => opts.Ignore())
                .ForMember(x => x.EmailAddress, opts => opts.Ignore())
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<OrganizationViewModel, Organization>()
                 .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<Organization, OrganizationViewModel>()
                .ForMember(x => x.Latitude, opts => opts.MapFrom(src => src.Sites.SingleOrDefault(x => x.Type == Core.Domain.Enum.SiteTypes.Main).Latitude))
                .ForMember(x => x.Longitude, opts => opts.MapFrom(src => src.Sites.SingleOrDefault(x => x.Type == Core.Domain.Enum.SiteTypes.Main).Longitude))
                .ForMember(x => x.Address, opts => opts.MapFrom(src => $"{src.StreetAddress1}, {src.City}, {src.State} {src.Zip}"));
        }
    }
}
