using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services.MetricService
{
    public class AugmentObjectMediaVisitMetricService : IAugmentObjectMediaVisitMetricService
    {
        private IAugmentObjectMediaVisitMetricRepository _augmentObjectMediaVisitMetricRepository;
        private IAugmentObjectRepository _augmentObjectRepository;

        public AugmentObjectMediaVisitMetricService(IAugmentObjectMediaVisitMetricRepository augmentObjectMediaVisitMetricRepository,
            IAugmentObjectRepository augmentObjectRepository)
        {
            _augmentObjectMediaVisitMetricRepository = augmentObjectMediaVisitMetricRepository;
            _augmentObjectRepository = augmentObjectRepository;
        }

        public Task<AugmentObjectMediaVisitMetric> CreateAugmentObjectMediaVisitMetricAsync(AugmentObjectMediaVisitMetric entity)
        {
            return _augmentObjectMediaVisitMetricRepository.CreateAugmentObjectMediaVisitMetricAsync(entity);
        }

        public async Task<AugmentObjectMediaVisitMetric> CreateAugmentObjectMediaVisitMetricAsync(AugmentObjectMediaVisitMetric entity, Guid mediaResourceId)
        {
            var existingAugmentObject = await _augmentObjectRepository.GetAugmentObjectByIdithChildrenAsNoTrackingAsync(entity.AugmentObjectId);

            if (existingAugmentObject == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            var augmentObjectMediaId = Guid.Empty;

            var augmentObjectMedia = existingAugmentObject.AugmentObjectMedias.SingleOrDefault(x => x.AudioId == mediaResourceId || x.VideoId == mediaResourceId);
            if (augmentObjectMedia == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            entity.AugmentObjectMediaId = augmentObjectMedia.Id;
            entity.Id = SequentialGuidGenerator.GenerateComb();

            return await _augmentObjectMediaVisitMetricRepository.CreateAugmentObjectMediaVisitMetricAsync(entity);
        }
    }
}
