using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IVideoRepository
    {
        Task<Video> AddVideoAsync(Video video);
        Task<Video> GetVideoByIdAsync(Guid videoId);
        Task<List<Video>> GetVideosByOrganizationIdAsync(Guid organizationId);
        Task<Video> UpdateVideoAsync(Guid audioId, Video entity);
    }
}
