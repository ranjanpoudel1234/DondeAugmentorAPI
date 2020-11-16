using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Web.ViewModels.V2.RolesAndPermission;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.RoleAndPermission
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>()
              .ForMember(x => x.CreatedDateUtc, opts => opts.MapFrom(src => src.AddedDate))
              .ForMember(x => x.Permissions, opts => opts.MapFrom(src => src.Permissions));
        }
    }
}
