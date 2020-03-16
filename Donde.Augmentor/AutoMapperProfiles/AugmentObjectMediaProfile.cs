using AutoMapper;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Web.ViewModels.AugmentObject;

namespace Donde.Augmentor.Web.AutoMapperProfiles
{
    public class AugmentObjectMediaProfile : Profile
    {
        public AugmentObjectMediaProfile()
        {
            CreateMap<AugmentObjectMediaViewModel, AugmentObjectMedia>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()))
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsActive, opts => opts.Ignore())
                .ForMember(x => x.Audio, opts => opts.Ignore())
                .ForMember(x => x.Video, opts => opts.Ignore())
                .ForMember(x => x.Avatar, opts => opts.Ignore())
                .ForMember(x => x.AugmentObject, opts => opts.Ignore());

            CreateMap<AugmentObjectMedia, AugmentObjectMediaViewModel>();
        }
    }
}
