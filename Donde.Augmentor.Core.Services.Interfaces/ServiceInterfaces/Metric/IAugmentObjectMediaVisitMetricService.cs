using Donde.Augmentor.Core.Domain.Models.Metrics;
using System;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric
{
    public interface IAugmentObjectMediaVisitMetricService
    {
        Task<AugmentObjectMediaVisitMetric> CreateAugmentObjectMediaVisitMetricAsync(AugmentObjectMediaVisitMetric entity);
        Task<AugmentObjectMediaVisitMetric> CreateAugmentObjectMediaVisitMetricAsync(AugmentObjectMediaVisitMetric entity, Guid mediaResourceId);
    }
}
