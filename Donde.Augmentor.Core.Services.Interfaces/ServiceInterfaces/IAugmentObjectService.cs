using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IAugmentObjectService
    {
        IQueryable<AugmentObject> GetAugmentObjectsQueryableWithChildren();
        IQueryable<AugmentObjectDto> GetAugmentObjects();
        Task<IEnumerable<AugmentObjectDto>> GetGeographicalAugmentObjectsByRadius(Guid organizationId, double latitude, double longitude, int radiusInMeters);
        Task<AugmentObject> CreateAugmentObjectAsync(AugmentObject entity);
        Task<AugmentObject> UpdateAugmentObjectAsync(Guid entityId, AugmentObject entity);
        Task DeleteAugmentObjectAsync(Guid entityId);
    }
}
