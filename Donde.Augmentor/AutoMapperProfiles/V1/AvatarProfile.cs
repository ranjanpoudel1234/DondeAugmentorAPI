using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels.V1;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V1
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
