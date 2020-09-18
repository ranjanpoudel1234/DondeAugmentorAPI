using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IVideoService
    {
        Task<Video> AddVideoAsync(Video video);
        Task<Video> GetVideoByIdAsync(Guid videoId);
        Task DeleteVideosByOrganizationIdAsync(Guid organizationId);
    }
}
