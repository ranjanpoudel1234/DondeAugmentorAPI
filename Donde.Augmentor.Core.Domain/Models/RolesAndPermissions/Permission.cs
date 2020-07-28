using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Core.Domain.Models.RolesAndPermissions
{
    public class Permission : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public ResourceTypes Resource { get; set; }
        public string Description { get; set; }
        public ResourceActionTypes ResourceAction { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<RolePermission> Roles {get; set; }
    }
}
