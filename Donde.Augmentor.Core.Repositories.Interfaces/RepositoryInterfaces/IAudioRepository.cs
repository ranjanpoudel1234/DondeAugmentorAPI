using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAudioRepository
    {
        IQueryable<Audio> GetAudios();
        Task<Audio> GetAudioByIdAsync(Guid audioId);
        Task<Audio> CreateAudioAsync(Audio entity);
        Task<List<Audio>> GetAudiosByOrganizationIdAsync(Guid organizationId);
        Task<Audio> UpdateAudioAsync(Guid audioId, Audio entity);
    }
}
