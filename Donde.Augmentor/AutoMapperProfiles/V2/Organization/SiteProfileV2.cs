using AutoMapper;
using Donde.Augmentor.Web.ViewModels.V2.Organization;
using System.Linq;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.Organization
{
    public class SiteProfileV2: Profile
    {
        public SiteProfileV2()
        {
            CreateMap<SiteViewModel, Core.Domain.Models.Site>()
              .ForMember(x => x.Id, opts => opts.Ignore())
              .ForMember(x => x.StreetAddress1, opts => opts.MapFrom(x => x.Location.StreetAddress1))
              .ForMember(x => x.StreetAddress2, opts => opts.MapFrom(x => x.Location.StreetAddress2))
              .ForMember(x => x.City, opts => opts.MapFrom(x => x.Location.City))
              .ForMember(x => x.State, opts => opts.MapFrom(x => x.Location.State))
              .ForMember(x => x.Zip, opts => opts.MapFrom(x => x.Location.Zip))
              .ForMember(x => x.Latitude, opts => opts.MapFrom(x => x.Location.Latitude))
              .ForMember(x => x.Longitude, opts => opts.MapFrom(x => x.Location.Longitude))
              .ForMember(x => x.AddedDate, opts => opts.Ignore())
              .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
              .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<Core.Domain.Models.Site, SiteViewModel>()
             .ForMember(x => x.Location, opts => opts.MapFrom(src => src));

            CreateMap<Core.Domain.Models.Site, SiteLocationViewModel>();

        }
    }
}
