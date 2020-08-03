using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Core.Domain.Validations;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.User;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Core.Services.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository, IMapper mapper,
            IValidator<User> validator, UserManager<User> userManager, IOrganizationService organizationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _userManager = userManager;
            _organizationService = organizationService;
        }

        public async Task<User> CreateAsync(User entity, string password)
        {
            entity.Id = SequentialGuidGenerator.GenerateComb();

            await _validator.ValidateOrThrowAsync(entity);
            CheckExistenceOfOrganizationsOrThrow(entity);

            //since this creation is not going through our normal pipeline, setting these values here.
            entity.AddedDate = DateTime.UtcNow;
            entity.Organizations.ForEach(x => x.AddedDate = DateTime.UtcNow);

            var result = await _userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
            {
                throw new HttpBadRequestException(string.Join(",", result.Errors.Select(x => x.Description)));
            }

            return entity;
        }

        public IQueryable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task<User> GetByIdAsync(Guid entityId)
        {
            return _userRepository.GetByIdAsync(entityId);
        }

        public Task<User> GetByIdWithNoTrackingAsync(Guid entityId)
        {
            return _userManager.FindByIdAsync(entityId.ToString());
        }

        public async Task<User> UpdateAsync(Guid entityId, User entity)
        {
            var existingUser = await _userManager.FindByIdAsync(entityId.ToString()); // using this instead of our repo
            // takes care of Id being tracked issue(when calling userManager.UpdateAsync below)

            if (existingUser == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            var mappedUser = _mapper.Map(entity, existingUser);

            await _validator.ValidateOrThrowAsync(mappedUser);
            CheckExistenceOfOrganizationsOrThrow(mappedUser);

            mappedUser.UpdatedDate = DateTime.UtcNow;
            mappedUser.Organizations.ForEach(x => x.UpdatedDate = DateTime.UtcNow);

            await _userRepository.UpdateUserOrganizationsAsync(mappedUser);
            var result = await _userManager.UpdateAsync(mappedUser);

            if (!result.Succeeded)
            {
                throw new HttpBadRequestException(string.Join(",", result.Errors.Select(x => x.Description)));
            }

            return await _userRepository.GetByIdAsync(mappedUser.Id);
        }

        public async Task<User> DeleteUserAsync(Guid entityId)
        {
            var existingUser = GetAll().SingleOrDefault(x => x.Id == entityId);

            if (existingUser == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            return await _userRepository.DeleteAsync(existingUser);
        }

        private void CheckExistenceOfOrganizationsOrThrow(User entity)
        {
            var userOrganizationIds = entity.Organizations.Select(x => x.OrganizationId).ToList();
            var userOrganizationsForIds = _organizationService.GetOrganizationByIds(userOrganizationIds).ToList();

            if (userOrganizationIds.Count != userOrganizationsForIds.Count)
            {
                throw new HttpBadRequestException(DondeErrorMessages.INVALID_ORGANIZATION_ID);
            }
        }
    }
}
