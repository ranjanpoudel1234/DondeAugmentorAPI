using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AudioProfile : Profile
    {
        public AudioProfile()
        {
            CreateMap<MediaAttachmentDto, Audio>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FileName))
                .ForMember(x => x.Url, opts => opts.MapFrom(src => src.FilePath))
                .ForMember(x => x.OrganizationId, opts => opts.Ignore())
                .ForMember(x => x.Organization, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<Audio, AudioViewModel>();
        }
    }
}
