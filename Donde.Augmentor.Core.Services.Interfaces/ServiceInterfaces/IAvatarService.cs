using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IAvatarService
    {
        IQueryable<Avatar> GetAvatars();
        Task<Avatar> GetAvatarByIdAsync(Guid avatarId);
        Task DeleteAvatarsByOrganizationIdAsync(Guid organizationId);
    }
}
