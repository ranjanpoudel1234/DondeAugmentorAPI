using Donde.Augmentor.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Core.Domain.Models.RolesAndPermissions
{
    public class Role : IDondeModel,  IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<RolePermission> Permissions { get; set; }
        public virtual ICollection<UserRole> Users { get; set; }
    }
}
