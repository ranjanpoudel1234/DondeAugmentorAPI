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
using System.Transactions;

namespace Donde.Augmentor.Core.Services.Services
{
    public class OrganizationService : IOrganizationService
    {
        private IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Organization> _validator;
        private readonly IOrganizationResourceService _organizationResourceService;
      

        public OrganizationService(IOrganizationRepository organizationRepository, 
            IMapper mapper, 
            IValidator<Organization> validator,
            IOrganizationResourceService organizationResourceService)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
            _validator = validator;
            _organizationResourceService = organizationResourceService;
        }

        public async Task<IEnumerable<Organization>> GetClosestOrganizationByRadius(double latitude, double longitude, int radiusInMeters)
        {
            return await _organizationRepository.GetClosestOrganizationByRadius(latitude, longitude, radiusInMeters);
        }

        public IQueryable<Organization> GetOrganizations(bool includeSites = false)
        {
            return _organizationRepository.GetOrganizations(includeSites);
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
            // delete audio, video, image, avatar, augmentobject
            using (var transaction = new CommittableTransaction())
            {
                var existingOrganization = GetOrganizations().SingleOrDefault(x => x.Id == entityId);

                if (existingOrganization == null)
                {
                    throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
                }

                await _organizationResourceService.DeleteOrganizationResourcesByOrganizationIdAsync(existingOrganization.Id);

                existingOrganization.IsDeleted = true;

                var updatedOrganization = await _organizationRepository.UpdateOrganizationAsync(existingOrganization);

                transaction.Commit();

                return updatedOrganization;

            }             
        }
    }
}
