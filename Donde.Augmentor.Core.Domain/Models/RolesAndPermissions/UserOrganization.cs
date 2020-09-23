using Donde.Augmentor.Core.Domain.Interfaces;
using System;

namespace Donde.Augmentor.Core.Domain.Models.RolesAndPermissions
{
    public class UserOrganization : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; } 
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
