using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class AvatarService : IAvatarService
    {
        private IAvatarRepository _avatarRepository;

        public AvatarService(IAvatarRepository avatarRepository)
        {
            _avatarRepository = avatarRepository;
        }

        public Task<Avatar> GetAvatarByIdAsync(Guid avatarId)
        {
            return _avatarRepository.GetAvatarByIdAsync(avatarId);
        }

        public IQueryable<Avatar> GetAvatars()
        {
            return _avatarRepository.GetAvatars();
        }
    }
}
