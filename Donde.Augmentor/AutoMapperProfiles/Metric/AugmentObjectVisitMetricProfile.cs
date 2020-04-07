using AutoMapper;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Web.ViewModels;

namespace Donde.Augmentor.Web.AutoMapperProfiles.Metric
{
    public class AugmentObjectVisitMetricProfile : Profile
    {
        public AugmentObjectVisitMetricProfile()
        {
            CreateMap<AugmentObjectVisitMetricViewModel, AugmentObjectVisitMetric>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => SequentialGuidGenerator.GenerateComb()))
                .ForMember(x => x.AugmentObject, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<AugmentObjectVisitMetric, AugmentObjectVisitMetricViewModel>();
        }
    }
}
