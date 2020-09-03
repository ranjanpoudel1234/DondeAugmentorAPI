using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using System;
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
            return GetAll<Avatar>();
        }
    }
}
