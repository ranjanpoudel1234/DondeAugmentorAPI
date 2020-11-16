using AutoMapper;
using System.Collections.Generic;

namespace Donde.Augmentor.Core.Domain.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.Identity.User, Models.Identity.User>()
                .ForMember(x => x.Organizations, src => src.MapFrom(x => x.Organizations))
                .ForMember(x => x.Roles, src => src.MapFrom(x => x.Roles));

            CreateMap<List<Models.RolesAndPermissions.UserOrganization>, List<Models.RolesAndPermissions.UserOrganization>>();
            CreateMap<List<Models.RolesAndPermissions.UserRole>, List<Models.RolesAndPermissions.UserRole>>();
        }
    }
}
