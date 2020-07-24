using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Web.ViewModels.V2.Organization;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.Organization
{
    public class OrganizationProfileV2 : Profile
    {
        public OrganizationProfileV2()
        {
            CreateMap<OrganizationViewModel, Core.Domain.Models.Organization>()
                .ForMember(x => x.Id, opts => opts.MapFrom(x => SequentialGuidGenerator.GenerateComb()))
                .ForMember(x => x.StreetAddress1, opts => opts.MapFrom(x => x.Address.StreetAddress1))
                .ForMember(x => x.StreetAddress2, opts => opts.MapFrom(x => x.Address.StreetAddress2))
                .ForMember(x => x.City, opts => opts.MapFrom(x => x.Address.City))
                .ForMember(x => x.State, opts => opts.MapFrom(x => x.Address.State))
                .ForMember(x => x.Zip, opts => opts.MapFrom(x => x.Address.Zip))
                .ForMember(x => x.LogoUrl, opts => opts.MapFrom(x => x.Logo.Url))
                .ForMember(x => x.LogoName, opts => opts.MapFrom(x => x.Logo.Name))
                .ForMember(x => x.LogoMimeType, opts => opts.MapFrom(x => x.Logo.MimeType))
                .ForMember(x => x.Sites, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<Core.Domain.Models.Organization, OrganizationViewModel>()
                .ForMember(x => x.CreatedDateUtc, opts => opts.MapFrom(src => src.AddedDate))
                .ForMember(x => x.Address, opts => opts.MapFrom(src => src))
                .ForMember(x => x.Logo, opts => opts.MapFrom(src => src));

            CreateMap<Core.Domain.Models.Organization, AddressViewModel>()
                .ForMember(x => x.StreetAddress1, opts => opts.MapFrom(src => src.StreetAddress1))
                .ForMember(x => x.StreetAddress2, opts => opts.MapFrom(src => src.StreetAddress2))
                .ForMember(x => x.City, opts => opts.MapFrom(src => src.City))
                .ForMember(x => x.State, opts => opts.MapFrom(src => src.State))
                .ForMember(x => x.Zip, opts => opts.MapFrom(src => src.Zip));

            CreateMap<Core.Domain.Models.Organization, OrganizationLogoMetadataViewModel>()
              .ForMember(x => x.Name, opts => opts.MapFrom(src => src.LogoName))
              .ForMember(x => x.Url, opts => opts.MapFrom(src => src.LogoUrl))
              .ForMember(x => x.MimeType, opts => opts.MapFrom(src => src.LogoMimeType));

            CreateMap<MediaAttachmentDto, Core.Domain.Models.Organization>()
                .ForMember(x => x.LogoName, opts => opts.MapFrom(src => src.FileName))
                .ForMember(x => x.LogoUrl, opts => opts.MapFrom(src => src.FilePath))
                .ForMember(x => x.LogoMimeType, opts => opts.MapFrom(src => src.MimeType))
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Name, opts => opts.Ignore())
                .ForMember(x => x.ShortName, opts => opts.Ignore())
                .ForMember(x => x.StreetAddress1, opts => opts.Ignore())
                .ForMember(x => x.StreetAddress2, opts => opts.Ignore())
                .ForMember(x => x.City, opts => opts.Ignore())
                .ForMember(x => x.State, opts => opts.Ignore())
                .ForMember(x => x.Zip, opts => opts.Ignore())
                .ForMember(x => x.Type, opts => opts.Ignore())
                .ForMember(x => x.Sites, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());
        }
    }
}
