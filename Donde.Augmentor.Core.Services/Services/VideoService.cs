using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class VideoService : IVideoService
    {
        private IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public Task<Video> AddVideoAsync(Video video)
        {
            return _videoRepository.AddVideoAsync(video);
        }
    }
}
