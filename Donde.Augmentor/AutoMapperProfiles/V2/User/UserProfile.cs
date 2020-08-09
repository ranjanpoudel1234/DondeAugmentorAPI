using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Web.ViewModels.V2.User;
using System.Linq;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Core.Domain.Models.Identity.User, UserViewModel>()
                .ForMember(x => x.OrganizationIds, opts => opts.MapFrom(src => src.Organizations.Select(x => x.OrganizationId)))
                .ForMember(x => x.RoleIds, opts => opts.MapFrom(src => src.Roles.Select(x => x.RoleId)))
                .ForMember(x => x.CreatedDateUtc, opts => opts.MapFrom(src => src.AddedDate))
                .ForMember(x => x.UpdatedDateUtc, opts => opts.MapFrom(src => src.UpdatedDate))
                .ForMember(x => x.EmailAddress, opts => opts.MapFrom(src => src.Email))
                .ForMember(x => x.Password, opts => opts.Ignore());

            CreateMap<UserViewModel, Core.Domain.Models.Identity.User>()
              .ForMember(x => x.Organizations, opts => opts.MapFrom(src => src.OrganizationIds.Select(x => new UserOrganization { OrganizationId = x })))
              .ForMember(x => x.Roles, opts => opts.MapFrom(src => src.RoleIds.Select(x => new UserRole { RoleId = x })))
              .ForMember(x => x.Email, opts => opts.MapFrom(src => src.EmailAddress))
              .ForMember(x => x.FullName, opts => opts.MapFrom(src => $"{src.FirstName} {src.LastName}"))
              .ForMember(x => x.AddedDate, opts => opts.Ignore())
              .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
              .ForMember(x => x.NormalizedUserName, opts => opts.Ignore())
              .ForMember(x => x.PasswordHash, opts => opts.Ignore())
              .ForMember(x => x.NormalizedEmail, opts => opts.Ignore())
              .ForMember(x => x.SecurityStamp, opts => opts.Ignore())
              .ForMember(x => x.ConcurrencyStamp, opts => opts.Ignore())
              .ForMember(x => x.LockoutEnabled, opts => opts.Ignore())
              .ForMember(x => x.LockoutEnd, opts => opts.Ignore())
              .ForMember(x => x.AccessFailedCount, opts => opts.Ignore());
              
        }
    }
}
