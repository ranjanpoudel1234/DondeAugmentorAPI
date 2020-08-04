using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels.V2.RolesAndPermission
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
