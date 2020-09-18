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
    public class VideoRepository : GenericRepository, IVideoRepository
    {
        public VideoRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public Task<Video> AddVideoAsync(Video video)
        {
            return CreateAsync<Video>(video);
        }

        public Task<Video> GetVideoByIdAsync(Guid videoId)
        {
            return GetByIdAsync<Video>(videoId);
        }

        public Task<Video> UpdateVideoAsync(Guid id, Video entity)
        {
            return UpdateAsync(id, entity);
        }

        public async Task<List<Video>> GetVideosByOrganizationIdAsync(Guid organizationId)
        {
            return await GetAll<Video>().Where(a => a.OrganizationId == organizationId).ToListAsync();
        }
    }
}
