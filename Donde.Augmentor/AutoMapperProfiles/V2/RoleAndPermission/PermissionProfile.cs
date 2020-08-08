using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Web.ViewModels.V2.RolesAndPermission;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.RoleAndPermission
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<RolePermission, PermissionViewModel>()
              .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Permission.Id))
              .ForMember(x => x.Resource, opts => opts.MapFrom(src => src.Permission.Resource))
              .ForMember(x => x.Description, opts => opts.MapFrom(src => src.Permission.Description))
              .ForMember(x => x.Action, opts => opts.MapFrom(src => src.Permission.ResourceAction));
        }
    }
}
