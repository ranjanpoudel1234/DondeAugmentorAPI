using Donde.Augmentor.Core.Domain.Enum;
using System;

namespace Donde.Augmentor.Web.ViewModels.V2.RolesAndPermission
{
    public class PermissionViewModel
    {
        public Guid Id { get; set; }
        public ResourceTypes Resource { get; set; }
        public string Description { get; set; }
        public ResourceActionTypes Action { get; set; }
    }
}
