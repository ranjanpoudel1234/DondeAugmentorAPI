using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Core.Domain.Validations;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.User;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Donde.Augmentor.Core.Services.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<User> validator, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _userManager = userManager;
        }

        public async Task<User> CreateAsync(User entity, string password)
        {
            entity.Id = SequentialGuidGenerator.GenerateComb();
            await _validator.ValidateOrThrowAsync(entity);

            var result = await _userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
            {
                throw new HttpBadRequestException(string.Join(",", result.Errors.Select(x => x.Description)));
            }

            return entity;

            // return await _userRepository.CreateAsync(entity);
        }

        public IQueryable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task<User> GetByIdAsync(Guid entityId)
        {
            return _userRepository.GetByIdAsync(entityId);
        }

        public async Task<User> UpdateAsync(Guid entityId, User entity)
        {
            var existingUser = GetAll().SingleOrDefault(x => x.Id == entityId);

            if (existingUser == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            var mappedOrganization = _mapper.Map(entity, existingUser);

            await _validator.ValidateOrThrowAsync(entity);
            return await _userRepository.UpdateAsync(entity);
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
    }
}
