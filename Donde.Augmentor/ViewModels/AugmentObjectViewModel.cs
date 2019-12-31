using Donde.Augmentor.Core.Domain.Enum;
using System;

namespace Donde.Augmentor.Web.ViewModels
{
    public class AugmentObjectViewModel
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

        public AugmentObjectMediaTypes MediaType { get; set; }
        public Guid? AvatarId { get; set; }
        public string AvatarName { get; set; }
        public string AvatarUrl { get; set; }
        public Guid? AudioId { get; set; }
        public string AudioName { get; set; }
        public string AudioUrl { get; set; }
        public Guid? VideoId { get; set; }
        public string VideoName { get; set; }
        public string VideoUrl { get; set; }

        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

    }
}
