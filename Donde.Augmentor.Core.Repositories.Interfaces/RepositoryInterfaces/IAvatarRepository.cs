using System.Linq;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAvatarRepository
    {
        IQueryable<Domain.Models.Avatar> GetAvatars();
    }
}
