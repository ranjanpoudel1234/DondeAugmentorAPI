using System.Threading.Tasks;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;
using Donde.Augmentor.Infrastructure.Database;

namespace Donde.Augmentor.Infrastructure.Repositories.MetricRepository
{
    public class AugmentObjectMediaVisitMetricRepository : GenericRepository, IAugmentObjectMediaVisitMetricRepository
    {
        public AugmentObjectMediaVisitMetricRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public Task<AugmentObjectMediaVisitMetric> CreateAugmentObjectMediaVisitMetricAsync(AugmentObjectMediaVisitMetric entity)
        {
            return CreateAsync(entity);
        }
    }
}
