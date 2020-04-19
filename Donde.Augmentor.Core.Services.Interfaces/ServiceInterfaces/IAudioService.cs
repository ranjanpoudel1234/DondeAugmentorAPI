using Donde.Augmentor.Core.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IAudioService
    {
        IQueryable<Audio> GetAudios();
        Task<Audio> AddAudioAsync(Audio audio);
    }
}
