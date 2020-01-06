using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Core.Domain.Models
{
    //@todo remember, on augmentObject post, validate that it can either have audio or video, but cant have both(may be later)
    public class AugmentObject : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public Guid AugmentImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
     
        public AugmentObjectTypes Type { get; set; }
        public Guid OrganizationId { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }

        public AugmentImage AugmentImage { get; set; }
        public Organization Organization { get; set; }
        public List<AugmentObjectMedia> AugmentObjectMedias { get; set; } = new List<AugmentObjectMedia>();
        public List<AugmentObjectLocation> AugmentObjectLocations { get; set; } = new List<AugmentObjectLocation>();
    }
}
