using AutoMapper;
using Donde.Augmentor.Web.ViewModels.V2.Organization;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.Organization
{
    public class OrganizationProfileV2 : Profile
    {
        public OrganizationProfileV2()
        {
            CreateMap<OrganizationViewModel, Core.Domain.Models.Organization>()
                .ForMember(x => x.StreetAddress1, opts => opts.MapFrom(x => x.Address.StreetAddress1))
                .ForMember(x => x.StreetAddress2, opts => opts.MapFrom(x => x.Address.StreetAddress2))
                .ForMember(x => x.City, opts => opts.MapFrom(x => x.Address.City))
                .ForMember(x => x.State, opts => opts.MapFrom(x => x.Address.State))
                .ForMember(x => x.Zip, opts => opts.MapFrom(x => x.Address.Zip))
                .ForMember(x => x.Sites, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<Core.Domain.Models.Organization, OrganizationViewModel>()
                .ForMember(x => x.CreatedDateUtc, opts => opts.MapFrom(src => src.AddedDate))
                .ForMember(x => x.Address, opts => opts.MapFrom(src => src));


            CreateMap<Core.Domain.Models.Organization, AddressViewModel>()
                .ForMember(x => x.StreetAddress1, opts => opts.MapFrom(src => src.StreetAddress1))
                .ForMember(x => x.StreetAddress2, opts => opts.MapFrom(src => src.StreetAddress2))
                .ForMember(x => x.City, opts => opts.MapFrom(src => src.City))
                .ForMember(x => x.State, opts => opts.MapFrom(src => src.State))
                .ForMember(x => x.Zip, opts => opts.MapFrom(src => src.Zip));
        }
    }
}
