using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Web.ViewModels.V2.Organization;
using System.IO;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.Organization
{
    public class OrganizationProfileV2 : Profile
    {
        public OrganizationProfileV2()
        {
            CreateMap<OrganizationViewModel, Core.Domain.Models.Organization>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.StreetAddress1, opts => opts.MapFrom(x => x.Address.StreetAddress1))
                .ForMember(x => x.StreetAddress2, opts => opts.MapFrom(x => x.Address.StreetAddress2))
                .ForMember(x => x.City, opts => opts.MapFrom(x => x.Address.City))
                .ForMember(x => x.State, opts => opts.MapFrom(x => x.Address.State))
                .ForMember(x => x.Zip, opts => opts.MapFrom(x => x.Address.Zip))
                .ForMember(x => x.LogoName, opts => opts.Ignore())
                .ForMember(x => x.LogoMimeType, opts => opts.Ignore())
                .ForMember(x => x.LogoFileId, opts => opts.Ignore())
                .ForMember(x => x.LogoExtension, opts => opts.Ignore())
                .ForMember(x => x.Sites, opts => opts.Ignore())
                .ForMember(x => x.Users, opts => opts.Ignore())
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
              .ForMember(x => x.Url, opts => opts.Ignore())
              .ForMember(x => x.ThumbnailUrl, opts => opts.Ignore())
              .ForMember(x => x.MimeType, opts => opts.MapFrom(src => src.LogoMimeType))
              .ForMember(x => x.FileId, opts => opts.MapFrom(src => src.LogoFileId))
              .ForMember(x => x.FileExtension, opts => opts.MapFrom(src => src.LogoExtension));

            CreateMap<MediaAttachmentDto, Core.Domain.Models.Organization>()
                .ForMember(x => x.LogoName, opts => opts.MapFrom(src => src.FileName))
                .ForMember(x => x.LogoMimeType, opts => opts.MapFrom(src => src.MimeType))
                .ForMember(x => x.LogoFileId, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.LogoExtension, opts => opts.MapFrom(src => Path.GetExtension(src.FilePath)))
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
                .ForMember(x => x.Users, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());
        }
    }
}
