using Donde.Augmentor.Core.Domain.Models;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IVideoRepository
    {
        Task<Video> AddVideoAsync(Video video);
    }
}
