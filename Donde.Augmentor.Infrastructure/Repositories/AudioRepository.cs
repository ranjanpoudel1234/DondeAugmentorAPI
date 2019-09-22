using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class AudioRepository : GenericRepository, IAudioRepository
    {
        public AudioRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public async Task<Audio> CreateAudioAsync(Audio entity)
        {
            return await CreateAsync(entity);
        }

        public IQueryable<Audio> GetAudios()
        {
            return GetAll<Audio>();
        }

        public async Task<Audio> UpdateAudioAsync(Guid id, Audio entity)
        {
            return await UpdateAsync(id, entity);
        }

    }
}
