using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAvatarRepository
    {
        IQueryable<Domain.Models.Avatar> GetAvatars();
        Task<Avatar> GetAvatarByIdAsync(Guid avatarId);
        Task<List<Avatar>> GetAvatarsByOrganizationIdAsync(Guid organizationId);
        Task<Avatar> UpdateAvatarAsync(Guid audioId, Avatar entity);
    }
}
