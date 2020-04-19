using Donde.Augmentor.Core.Domain.Models.Metrics;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric
{
    public interface IAugmentObjectMediaVisitMetricRepository
    {
        Task<AugmentObjectMediaVisitMetric> CreateAugmentObjectMediaVisitMetricAsync(AugmentObjectMediaVisitMetric entity);
    }
}
