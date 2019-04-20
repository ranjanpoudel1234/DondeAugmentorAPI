using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class AugmentObjectService : IAugmentObjectService
    {
        private IAugmentObjectRepository _augmentObjectRepository;

        public AugmentObjectService(IAugmentObjectRepository augmentObjectRepository)
        {
            _augmentObjectRepository = augmentObjectRepository;
        }

        public IQueryable<AugmentObjectDto> GetAugmentObjects()
        {
           return _augmentObjectRepository.GetAugmentObjects();
        }

        public async Task<AugmentObject> CreateAugmentObjectAsync(AugmentObject entity)
        {
            return await _augmentObjectRepository.CreateAugmentObjectAsync(entity);
        }

        public async Task<IEnumerable<AugmentObjectDto>> GetClosestAugmentObjectsByRadius(double latitude, double longitude, int radiusInMeters)
        {
            return await _augmentObjectRepository.GetClosestAugmentObjectsByRadius(latitude, longitude, radiusInMeters);
        }

        public async Task<AugmentObject> UpdateAugmentObjectAsync(Guid id, AugmentObject entity)
        {
            return await _augmentObjectRepository.UpdateAugmentObjectAsync(id, entity);
        }
    }
}
