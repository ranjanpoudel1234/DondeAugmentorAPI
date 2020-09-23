using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IAugmentImageService
    {
        IQueryable<AugmentImage> GetAugmentImages();
        Task<AugmentImage> AddAugmentImageAsync(AugmentImage augmentImage);
        Task<AugmentImage> GetAugmentImageByIdAsync(Guid augmentImageId);
        Task DeleteAugmentImagesByOrganizationIdAsync(Guid organizationId);
    }
}
