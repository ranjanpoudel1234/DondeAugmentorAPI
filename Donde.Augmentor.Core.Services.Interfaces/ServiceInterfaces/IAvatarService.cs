using Donde.Augmentor.Core.Domain.Models;
using System.Linq;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IAvatarService
    {
        IQueryable<Avatar> GetAvatars();
    }
}
