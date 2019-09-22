using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
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
    }
}
