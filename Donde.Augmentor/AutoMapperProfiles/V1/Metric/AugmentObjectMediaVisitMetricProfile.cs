using AutoMapper;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1.Metric;

namespace Donde.Augmentor.Web.AutoMapperProfiles.Metric
{
    public class AugmentObjectMediaVisitMetricProfile : Profile
    {
        public AugmentObjectMediaVisitMetricProfile()
        {
            CreateMap<AugmentObjectMediaVisitMetricViewModel, AugmentObjectMediaVisitMetric>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()))
                .ForMember(x => x.AugmentObject, opts => opts.Ignore())
                .ForMember(x => x.AugmentObjectMedia, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<AugmentObjectMediaVisitMetric, AugmentObjectMediaVisitMetricViewModel>();
        }
    }
}
