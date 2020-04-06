using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services.Metric
{
    public class AugmentObjectMediaVisitMetricService : IAugmentObjectMediaVisitMetricService
    {
        private IAugmentObjectMediaVisitMetricRepository _augmentObjectMediaVisitMetricRepository;

        public AugmentObjectMediaVisitMetricService(IAugmentObjectMediaVisitMetricRepository augmentObjectMediaVisitMetricRepository)
        {
            _augmentObjectMediaVisitMetricRepository = augmentObjectMediaVisitMetricRepository;
        }

        public Task<AugmentObjectMediaVisitMetric> CreateAugmentObjectMediaVisitMetricAsync(AugmentObjectMediaVisitMetric entity)
        {
            return _augmentObjectMediaVisitMetricRepository.CreateAugmentObjectMediaVisitMetricAsync(entity);
        }
    }
}
