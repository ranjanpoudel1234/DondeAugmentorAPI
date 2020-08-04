using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Web.ViewModels.V2.RolesAndPermission;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.RoleAndPermission
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionViewModel>()
              .ForMember(x => x.Action, opts => opts.MapFrom(src => src.ResourceAction));
        }
    }
}
