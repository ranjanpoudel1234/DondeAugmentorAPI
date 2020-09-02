using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAugmentObjectRepository
    {
        IQueryable<AugmentObject> GetAugmentObjectsQueryableWithChildren();
        AugmentObject GetAugmentObjectByIdWithChildren(Guid id);
        IQueryable<AugmentObjectDto> GetAugmentObjects();
        Task<IEnumerable<AugmentObjectDto>> GetGeographicalAugmentObjectsByRadius(Guid organizationId, double latitude, double longitude, int radiusInMeters);
        Task<AugmentObject> CreateAugmentObjectAsync(AugmentObject entity);
        Task<AugmentObject> UpdateAugmentObjectAsync(Guid id, AugmentObject entity);
    }
}
