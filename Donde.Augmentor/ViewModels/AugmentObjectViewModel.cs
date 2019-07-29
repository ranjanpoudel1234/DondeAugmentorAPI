using System;

namespace Donde.Augmentor.Web.ViewModels
{
    public class AugmentObjectViewModel
    {
        public Guid Id { get; set; }

        public Guid? AvatarId { get; set; }

        public Guid? AudioId { get; set; }

        public Guid? VideoId { get; set; }

        public Guid AugmentImageId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public Guid OrganizationId { get; set; }

        public double Distance { get; set; }


        public string AudioName { get; set; }
        public string AudioUrl { get; set; }

        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

        public string VideoName { get; set; }
        public string VideoUrl { get; set; }
    }
}
