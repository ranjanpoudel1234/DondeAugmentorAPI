using Donde.Augmentor.Core.Domain.Interfaces;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Core.Domain.Models.Identity
{
    public class User : IdentityUser<Guid>, IAuditFieldsModel, IDondeModel
    {
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public List<UserOrganization> Organizations { get; set; }
        public List<UserRole> Roles { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
