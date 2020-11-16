using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1;
using System.IO;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<MediaAttachmentDto, Video>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FileName))
                .ForMember(x => x.FileId, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.Extension, opts => opts.MapFrom(src => Path.GetExtension(src.FilePath)))
                .ForMember(x => x.OrganizationId, opts => opts.Ignore())
                .ForMember(x => x.Organization, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<Video, VideoViewModel>()
                .ForMember(x => x.Url, opts => opts.Ignore());
        }
    }
}
