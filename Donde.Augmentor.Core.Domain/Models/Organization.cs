using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Interfaces;
using System;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class Organization : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }

        public OrganizationType OrganizationType { get; set;}
    }
}
