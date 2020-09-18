using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class AugmentImageRepository : GenericRepository, IAugmentImageRepository
    {
        public AugmentImageRepository(DondeContext dbContext) : base(dbContext)
        {

        }
    
        public IQueryable<AugmentImage> GetAugmentImages()
        {
            return GetAll<AugmentImage>();
        }

        public async Task<AugmentImage> AddAugmentImageAsync(AugmentImage augmentImage)
        {
            return await CreateAsync(augmentImage);
        }

        public Task<AugmentImage> GetAugmentImageByIdAsync(Guid augmentImageId)
        {
            return GetByIdAsync<AugmentImage>(augmentImageId);
        }

        public Task<AugmentImage> UpdateAugmentImageAsync(Guid id, AugmentImage entity)
        {
            return UpdateAsync(id, entity);
        }

        public async Task<List<AugmentImage>> GetAugmentImagesByOrganizationIdAsync(Guid organizationId)
        {
            return await GetAll<AugmentImage>().Where(a => a.OrganizationId == organizationId).ToListAsync();
        }
    }
}
