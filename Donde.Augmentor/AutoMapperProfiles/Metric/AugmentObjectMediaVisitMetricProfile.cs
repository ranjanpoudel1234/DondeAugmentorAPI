using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.AutoMapperProfiles.Metric
{
    public class AugmentObjectMediaVisitMetricProfile : Profile
    {
        public AugmentObjectMediaVisitMetricProfile()
        {
            CreateMap<AugmentObjectMediaVisitMetricViewModel, AugmentObjectMediaVisitMetric>()
                .ForMember(x => x.AugmentObject, opts => opts.Ignore())
                .ForMember(x => x.AugmentObjectMedia, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<AugmentObjectMediaVisitMetric, AugmentObjectMediaVisitMetricViewModel>();
        }
    }
}
