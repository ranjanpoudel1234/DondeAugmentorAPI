using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System.Linq;

namespace Donde.Augmentor.Core.Services.Services
{
    public class AudioService : IAudioService
    {
        private IAudioRepository _audioRepository;

        public AudioService(IAudioRepository AudioRepository)
        {
            _audioRepository = AudioRepository;
        }

        public IQueryable<Audio> GetAudios()
        {
            return _audioRepository.GetAudios();           
        }
    }
}
