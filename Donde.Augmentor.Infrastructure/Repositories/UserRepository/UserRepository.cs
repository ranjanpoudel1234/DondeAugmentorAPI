using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.User;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        public UserRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<User> GetAll()
        {
            return GetAllAsNoTracking<User>()
                .Include(x => x.Organizations)
                .Include(user => user.Roles)
                .ThenInclude(userRole => userRole.Role);
        }

        public Task<User> GetByIdAsync(Guid entityId)
        {
            return GetAllAsNoTracking<User>()
                .Include(x => x.Organizations)
                .Include(user => user.Roles)
                .ThenInclude(userRole => userRole.Role).SingleOrDefaultAsync(x => x.Id == entityId);
        }

        public async Task<User> GetByIdWithNoTrackingAsync(Guid entityId)
        {
            var user = await _dbContext.Users.Include(x => x.Organizations).AsNoTracking().SingleOrDefaultAsync(x => x.Id == entityId);
            return user;
        }

        public async Task<User> CreateAsync(User entity)
        {
            return await CreateAsync(entity);
        }

        public async Task<User> DeleteAsync(User entity)
        {
            var existingUserOrganizations = _dbContext.UserOrganizations.Where(x => x.UserId == entity.Id && !x.IsDeleted).ToList();
            foreach (var userOrganization in existingUserOrganizations)
            {
                userOrganization.IsDeleted = true;
                await UpdateAsync(userOrganization.Id, userOrganization);
            }

            entity.IsDeleted = true;
            return await UpdateAsync(entity.Id, entity);
        }

        public async Task UpdateUserOrganizationsAsync(User entity)
        {
            var updatedUserOrganizationIds = entity.Organizations.Select(x => x.OrganizationId).ToList();
            var existingUserOrganizationIds = _dbContext.UserOrganizations.Where(x => x.UserId == entity.Id && !x.IsDeleted).Select(x => x.OrganizationId).ToList();

            var newUserOrganizationIds = updatedUserOrganizationIds.Where(x => !existingUserOrganizationIds.Contains(x));

            entity.Organizations = null; //set it null so EF does not add by default.

            foreach (var newOrgId in newUserOrganizationIds)
            {
                var userOrganization = new UserOrganization { Id = SequentialGuidGenerator.GenerateComb(), UserId = entity.Id, OrganizationId = newOrgId };

                await CreateAsync<UserOrganization>(userOrganization);              
            }

            var deletedOrganizationIds = existingUserOrganizationIds.Where(x => !updatedUserOrganizationIds.Contains(x));
            foreach (var deletedOrgId in deletedOrganizationIds)
            {
                var userOrganization = _dbContext.UserOrganizations.Single(x => x.UserId == entity.Id && x.OrganizationId == deletedOrgId && !x.IsDeleted);
                userOrganization.IsDeleted = true;

                await UpdateAsync<UserOrganization>(userOrganization.Id, userOrganization);
            }
        }

        public async Task UpdateUserRolesAsync(User entity)
        {
            var updatedUserRoleIds = entity.Roles.Select(x => x.RoleId).ToList();
            var existingUserRoleIds = _dbContext.UserRoles.Where(x => x.UserId == entity.Id && !x.IsDeleted).Select(x => x.RoleId).ToList();

            var newUserRoleIds = updatedUserRoleIds.Where(x => !existingUserRoleIds.Contains(x));

            entity.Roles = null; //set it null so EF does not add by default.

            foreach (var newRoleId in newUserRoleIds)
            {
                var userRole = new UserRole { Id = SequentialGuidGenerator.GenerateComb(), UserId = entity.Id, RoleId = newRoleId };

                await CreateAsync<UserRole>(userRole);
            }

            var deletedRoleIds = existingUserRoleIds.Where(x => !updatedUserRoleIds.Contains(x));
            foreach (var deletedRoleId in deletedRoleIds)
            {
                var userRole = _dbContext.UserRoles.Single(x => x.UserId == entity.Id && x.RoleId == deletedRoleId && !x.IsDeleted);
                userRole.IsDeleted = true;

                await UpdateAsync<UserRole>(userRole.Id, userRole);
            }
        }
    }
}
