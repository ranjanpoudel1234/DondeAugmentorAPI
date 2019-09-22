using Donde.Augmentor.Core.Domain.Interfaces;
using System;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class Audio : IDondeModel, IResourceModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public Guid OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        
    }
}
