using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Web.ViewModels.V2.Metric;

namespace Donde.Augmentor.Web.AutoMapperProfiles.V2.Metric
{
    public class TargetMediaVisitMetricProfile : Profile
    {
        public TargetMediaVisitMetricProfile()
        {
            CreateMap<TargetMediaVisitMetricViewModel, AugmentObjectMediaVisitMetric>()
                .ForMember(x => x.Id, opts => opts.Ignore()) // map in service
                .ForMember(x => x.AugmentObject, opts => opts.Ignore())
                .ForMember(x => x.AugmentObjectMediaId, opts => opts.Ignore()) // map in service
                .ForMember(x => x.AugmentObjectMedia, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<AugmentObjectMediaVisitMetric, TargetMediaVisitMetricViewModel>()
                  .ForMember(x => x.MediaId, opts => opts.Ignore());
        }
    }
}
