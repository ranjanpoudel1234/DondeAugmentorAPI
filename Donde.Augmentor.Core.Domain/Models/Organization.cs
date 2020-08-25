using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Interfaces;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class Organization : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string LogoName { get; set; }
        public string LogoMimeType { get; set; }
        public Guid LogoFileId { get; set; }
        public string LogoExtension { get; set; }
        public OrganizationTypes Type { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public List<Site> Sites { get; set; }
        public List<UserOrganization> Users { get; set; }
    }
}
