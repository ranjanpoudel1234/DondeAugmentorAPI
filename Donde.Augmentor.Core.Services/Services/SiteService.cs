using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Validations;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using FluentValidation;

namespace Donde.Augmentor.Core.Services.Services
{
    public class SiteService : ISiteService
    {
        private ISiteRepository _siteRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Site> _validator;
        public SiteService(ISiteRepository siteRepository, IMapper mapper, IValidator<Site> validator)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public IQueryable<Site> GetSites()
        {
            return _siteRepository.GetSites();
        }

        public async Task<Site> CreateSiteAsync(Site entity)
        {
            entity.Id = SequentialGuidGenerator.GenerateComb();
            await _validator.ValidateOrThrowAsync(entity);
            return await _siteRepository.CreateSiteAsync(entity);
        }
    }
}
