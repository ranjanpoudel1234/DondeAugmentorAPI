using Donde.Augmentor.Core.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class Avatar : IDondeModel, IResourceModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string TextureFileName { get; set; }
        public string TextureFileUrl { get; set; }

        [Column(TypeName = "jsonb")]
        public string AvatarConfiguration { get; set; }
        public string MimeType { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
