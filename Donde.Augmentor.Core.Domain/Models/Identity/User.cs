using Donde.Augmentor.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Donde.Augmentor.Core.Domain.Models.Identity
{
    public class User : IdentityUser, IAuditFieldsModel
    {
        public string FullName { get; set; }
     
        public Guid OrganizationId { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
