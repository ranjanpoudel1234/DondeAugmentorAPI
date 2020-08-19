using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AvatarProfile : Profile
    {
        public AvatarProfile()
        {        
            CreateMap<Avatar, AvatarViewModel>()
                  .ForMember(x => x.Url, opts => opts.Ignore());
        }
    }
}
