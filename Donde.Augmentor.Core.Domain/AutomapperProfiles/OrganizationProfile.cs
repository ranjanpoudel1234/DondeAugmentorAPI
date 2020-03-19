using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;

namespace Donde.Augmentor.Core.Domain.AutomapperProfiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, Organization>()
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsActive, opts => opts.Ignore());
        }
    }
}
