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
    public class AudioRepository : GenericRepository, IAudioRepository
    {
        public AudioRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public Task<Audio> CreateAudioAsync(Audio entity)
        {
            return  CreateAsync(entity);
        }

        public Task<Audio> GetAudioByIdAsync(Guid audioId)
        {
            return GetByIdAsync<Audio>(audioId);
        }

        public IQueryable<Audio> GetAudios()
        {
            return GetAll<Audio>();
        }

        public Task<Audio> UpdateAudioAsync(Guid id, Audio entity)
        {
            return UpdateAsync(id, entity);
        }

        public async Task<List<Audio>> GetAudiosByOrganizationIdAsync(Guid organizationId)
        {
            return await GetAll<Audio>().Where(a => a.OrganizationId == organizationId).ToListAsync();
        }
    }
}
