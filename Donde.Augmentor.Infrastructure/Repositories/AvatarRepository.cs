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
    public class AvatarRepository : GenericRepository, IAvatarRepository
    {
        public AvatarRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public Task<Avatar> GetAvatarByIdAsync(Guid avatarId)
        {
            return GetByIdAsync<Avatar>(avatarId);
        }

        public IQueryable<Avatar> GetAvatars()
        {
            return GetAllAsNoTracking<Avatar>();
        }

        public Task<Avatar> UpdateAvatarAsync(Guid id, Avatar entity)
        {
            return UpdateAsync(id, entity);
        }

        public async Task<List<Avatar>> GetAvatarsByOrganizationIdAsync(Guid organizationId)
        {
            return await GetAllAsNoTracking<Avatar>().Where(a => a.OrganizationId == organizationId).ToListAsync();
        }
    }
}
