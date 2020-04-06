using System.Threading.Tasks;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;

namespace Donde.Augmentor.Core.Services.Services.Metric
{
    public class AugmentObjectVisitMetricService : IAugmentObjectVisitMetricService
    {
        private IAugmentObjectVisitMetricRepository _augmentObjectVisitMetricRepository;

        public AugmentObjectVisitMetricService(IAugmentObjectVisitMetricRepository augmentObjectVisitMetricRepository)
        {
            _augmentObjectVisitMetricRepository = augmentObjectVisitMetricRepository;
        }

        public Task<AugmentObjectVisitMetric> CreateAugmentObjectVisitMetricAsync(AugmentObjectVisitMetric entity)
        {
            return _augmentObjectVisitMetricRepository.CreateAugmentObjectVisitMetricAsync(entity);
        }
    }
}
