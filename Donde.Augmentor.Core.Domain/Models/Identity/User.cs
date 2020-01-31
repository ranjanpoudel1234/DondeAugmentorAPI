using Donde.Augmentor.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Donde.Augmentor.Core.Domain.Models.Identity
{
    public class User : IdentityUser, IDondeModel, IAuditFieldsModel
    {
        public new Guid Id { set; get; }
        public string FullName { get; set; }
     
        public Guid OrganizationId { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
