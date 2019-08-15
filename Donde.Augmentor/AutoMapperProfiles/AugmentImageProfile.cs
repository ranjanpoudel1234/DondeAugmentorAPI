using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AugmentImageProfile : Profile
    {
        public AugmentImageProfile()
        {
            CreateMap<MediaAttachmentDto, AugmentImage>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FileName))
                .ForMember(x => x.Url, opts => opts.MapFrom(src => src.FilePath))
                .ForMember(x => x.OrganizationId, opts => opts.Ignore())
                .ForMember(x => x.Organization, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsActive, opts => opts.Ignore());

            CreateMap<AugmentImage, AugmentImageViewModel>();           
        }
    }
}
