using AutoMapper;
using Donde.Augmentor.Web.ViewModels.V2.User;
using System.Linq;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.User
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<Core.Domain.Models.Identity.User, UserInfoViewModel>()
                .ForMember(x => x.OrganizationIds, opts => opts.MapFrom(src => src.Organizations.Select(x => x.OrganizationId)))
                .ForMember(x => x.RoleNames, opts => opts.MapFrom(src => src.Roles.Select(x => x.Role.Name)))
                .ForMember(x => x.CreatedDateUtc, opts => opts.MapFrom(src => src.AddedDate))
                .ForMember(x => x.UpdatedDateUtc, opts => opts.MapFrom(src => src.UpdatedDate))
                .ForMember(x => x.EmailAddress, opts => opts.MapFrom(src => src.Email))
                .ForMember(x => x.Password, opts => opts.Ignore());
        }
    }
}
