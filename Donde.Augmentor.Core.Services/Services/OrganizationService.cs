using AutoMapper;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Validations;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class OrganizationService : IOrganizationService
    {
        private IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Organization> _validator;
        public OrganizationService(IOrganizationRepository organizationRepository, IMapper mapper, IValidator<Organization> validator)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<Organization>> GetClosestOrganizationByRadius(double latitude, double longitude, int radiusInMeters)
        {
            return await _organizationRepository.GetClosestOrganizationByRadius(latitude, longitude, radiusInMeters);
        }

        public IQueryable<Organization> GetOrganizations()
        {
            return _organizationRepository.GetOrganizations();
        }

        public IQueryable<Organization> GetOrganizationByIds(List<Guid> organizationIds)
        {
            return _organizationRepository.GetOrganizationByIds(organizationIds);
        }

        public Task<Organization> GetOrganizationByIdAsync(Guid organizationId)
        {
            return _organizationRepository.GetOrganizationByIdAsync(organizationId);
        }

        public async Task<Organization> CreateOrganizationAsync(Organization entity)
        {
            entity.Id = SequentialGuidGenerator.GenerateComb();
            await _validator.ValidateOrThrowAsync(entity, ruleSets: $"{OrganizationValidator.DefaultRuleSet}");
            return await _organizationRepository.CreateOrganizationAsync(entity);
        }

        public async Task<Organization> UpdateOrganizationAsync(Guid entityId, Organization entity)
        {
            var existingOrganization = GetOrganizations().SingleOrDefault(x => x.Id == entityId);

            if (existingOrganization == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            var mappedOrganization = _mapper.Map(entity, existingOrganization);

            await _validator.ValidateOrThrowAsync(entity, ruleSets: $"{OrganizationValidator.DefaultRuleSet},{OrganizationValidator.OrganizationUpdateRuleSet}");
            return await _organizationRepository.UpdateOrganizationAsync(mappedOrganization);
        }

        public async Task<Organization> DeleteOrganizationAsync(Guid entityId)
        {
            var existingOrganization = GetOrganizations().SingleOrDefault(x => x.Id == entityId);

            if (existingOrganization == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            existingOrganization.IsDeleted = true;

            return await _organizationRepository.UpdateOrganizationAsync(existingOrganization);
        }
    }
}
