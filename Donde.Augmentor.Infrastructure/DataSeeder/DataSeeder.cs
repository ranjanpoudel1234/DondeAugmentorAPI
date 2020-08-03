using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Donde.Augmentor.Infrastructure.Database;
using System;
using System.Collections.Generic;
using static Donde.Augmentor.Core.Domain.DomainConstants;
using System.Linq;
using Donde.Augmentor.Core.Domain.Extensions;

namespace Donde.Augmentor.Infrastructure.DataSeeder
{
    public static class DataSeeder
    {
        public static void SeedData()
        {
            using (var context = new DondeContext())
            {
                context.Database.EnsureCreated();

                SeedPermissions(context);
                SeedRolesAndPermissions(context);

                context.SaveChanges();
            }
        }

        private static void SeedPermissions(DondeContext context)
        {
            var allResourcesTypesWithDescription = Enum.GetValues(typeof(ResourceTypes)).Cast<ResourceTypes>().ToList();
            var allResourceActionTypes = Enum.GetValues(typeof(ResourceActionTypes)).Cast<ResourceActionTypes>().ToList();

            foreach (var resourceType in allResourcesTypesWithDescription)
            {
                foreach (var resourceActionType in allResourceActionTypes)
                {
                    var permission = new Permission
                    {
                        Id = SequentialGuidGenerator.GenerateComb(),
                        Resource = resourceType,
                        Description = $"{resourceActionType} {resourceType}",
                        ResourceAction = resourceActionType,
                        AddedDate = DateTime.UtcNow
                    };

                    var existingPermission = context.Permissions
                        .SingleOrDefault(x => x.Resource == permission.Resource && x.ResourceAction == permission.ResourceAction && !x.IsDeleted);

                    if(existingPermission == null)
                    {
                        context.Permissions.Add(permission);
                    }
                    else
                    {
                        existingPermission.Description = permission.Description;
                        existingPermission.UpdatedDate = DateTime.UtcNow;
                    }
                }
            }

            context.SaveChanges();
        }

        private static void SeedRolesAndPermissions(DondeContext context)
        {
            var rolesToAdd = new List<string> { Roles.ORGANIZATION_ADMINADMINISTRATOR, Roles.SUPER_ADMINADMINISTRATOR };
            foreach (var role in rolesToAdd)
            {
                var newRole = new Role();
                var newRoleId = SequentialGuidGenerator.GenerateComb();
                newRole.Id = newRoleId;
                newRole.Name = role;
                newRole.AddedDate = DateTime.UtcNow;

                var existingSameRole = context.Roles.SingleOrDefault(x => x.Name == role && !x.IsDeleted);

                if(existingSameRole == null)
                {
                    context.Roles.Add(newRole);
                }
                else
                {
                    existingSameRole.UpdatedDate = DateTime.UtcNow;
                }

                context.SaveChanges();

                SeedPermissionsForRole(context, role, newRoleId);
            }        
        }

        private static void SeedPermissionsForRole(DondeContext context, string role, Guid newRoleId)
        {
            var existingSameRole = context.Roles.SingleOrDefault(x => x.Name == role && !x.IsDeleted);

            var rolePermissions = new List<RolePermission>();
 
            if (role == Roles.ORGANIZATION_ADMINADMINISTRATOR)
            {
                var permissions = context.Permissions.Where(x => x.Resource == ResourceTypes.Targets && !x.IsDeleted);
                foreach (var permission in permissions)
                {
                    rolePermissions.Add(new RolePermission
                    {
                        Id = SequentialGuidGenerator.GenerateComb(),
                        RoleId = existingSameRole == null ? newRoleId : existingSameRole.Id,
                        PermissionId = permission.Id,
                        AddedDate = DateTime.UtcNow
                    });
                }        
            }
            else if (role == Roles.SUPER_ADMINADMINISTRATOR)
            {
                var permissions = context.Permissions.Where(x => !x.IsDeleted); // all permissions
                foreach (var permission in permissions)
                {
                    rolePermissions.Add(new RolePermission
                    {
                        Id = SequentialGuidGenerator.GenerateComb(),
                        RoleId = existingSameRole == null ? newRoleId : existingSameRole.Id,
                        PermissionId = permission.Id,
                        AddedDate = DateTime.UtcNow
                    });
                }
            }

            foreach (var rolePermission in rolePermissions)
            {
                var existingSameRolePermission = context.RolePermissions
                    .SingleOrDefault(x => x.RoleId == rolePermission.RoleId && x.PermissionId == rolePermission.PermissionId && !x.IsDeleted);

                if (existingSameRolePermission == null)
                {
                    context.RolePermissions.Add(rolePermission);
                }
                else
                {
                    existingSameRolePermission.UpdatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}
