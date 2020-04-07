using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;
using Donde.Augmentor.Infrastructure.Database;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories.MetricRepository
{
    public class AugmentObjectVisitMetricRepository : GenericRepository, IAugmentObjectVisitMetricRepository
    {
        public AugmentObjectVisitMetricRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public Task<AugmentObjectVisitMetric> CreateAugmentObjectVisitMetricAsync(AugmentObjectVisitMetric entity)
        {
            return CreateAsync(entity);
        }
    }
}
