using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<AugmentObject> CreateAugmentObjectAsync(AugmentObject entity)
        {
            return await _augmentObjectRepository.CreateAugmentObjectAsync(entity);
        }

        public async Task<IEnumerable<AugmentObjectDto>> GetAugmentObjectByImageId(double latitude, double longitude)
        {
            return await _augmentObjectRepository.GetAugmentObjectByImageId(latitude, longitude);
        }

        public async Task<AugmentObject> UpdateAugmentObjectAsync(Guid id, AugmentObject entity)
        {
            return await _augmentObjectRepository.UpdateAugmentObjectAsync(id, entity);
        }
    }
}
