using Donde.Augmentor.Core.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAudioRepository
    {
        IQueryable<Audio> GetAudios();
        Task<Audio> CreateAudioAsync(Audio entity);
    }
}
