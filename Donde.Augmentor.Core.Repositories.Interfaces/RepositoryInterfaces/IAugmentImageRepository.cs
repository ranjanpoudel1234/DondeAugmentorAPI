using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAugmentImageRepository
    {
        IQueryable<AugmentImage> GetAugmentImages();
        Task<AugmentImage> GetAugmentImageByIdAsync(Guid augmentImageId);
        Task<AugmentImage> AddAugmentImageAsync(AugmentImage augmentImage);
        Task<List<AugmentImage>> GetAugmentImagesByOrganizationIdAsync(Guid organizationId);
        Task<AugmentImage> UpdateAugmentImageAsync(Guid audioId, AugmentImage entity);
    }
}
