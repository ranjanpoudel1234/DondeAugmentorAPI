using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using System.Linq;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class AvatarRepository : GenericRepository, IAvatarRepository
    {
        public AvatarRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Avatar> GetAvatars()
        {
            return GetAll<Avatar>();
        }
    }
}
