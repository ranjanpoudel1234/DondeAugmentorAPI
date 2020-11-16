using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

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


        public Task<Audio> AddAudioAsync(Audio audio)
        {
            return _audioRepository.CreateAudioAsync(audio);
        }

        public Task<Audio> GetAudioByIdAsync(Guid audioId)
        {
            return _audioRepository.GetAudioByIdAsync(audioId);
        }

        public async Task DeleteAudiosByOrganizationIdAsync(Guid organizationId)
        {
            var audiosByOrganization = await _audioRepository.GetAudiosByOrganizationIdAsync(organizationId);
            foreach (var audio in audiosByOrganization)
            {
                audio.IsDeleted = true;
                await _audioRepository.UpdateAudioAsync(audio.Id, audio);
            }
        }
    }
}
