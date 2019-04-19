using Donde.Augmentor.Core.Domain.Interfaces;
using System;

namespace Donde.Augmentor.Core.Domain.Models
{
    //@todo remember, on augmentObject post, validate that it can either have audio or video, but cant have both(may be later)
    public class AugmentObject : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }

        public Guid AvatarId { get; set; }
        public Guid? AudioId { get; set; }
        public Guid? VideoId { get; set; }
        public Guid AugmentImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Guid OrganizationId { get; set; }

        public Avatar Avatar { get; set; }
        public Audio Audio { get; set; }
        public AugmentImage AugmentImage { get; set; }
        public Video Video { get; set; }
        public Organization Organization { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
