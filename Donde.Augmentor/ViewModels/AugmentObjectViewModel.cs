using System;

namespace Donde.Augmentor.Web.ViewModels
{
    public class AugmentObjectViewModel
    {
        public Guid Id { get; set; }

        public Guid AvatarId { get; set; }

        public Guid AudioId { get; set; }

        public Guid AugmentImageId { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Guid OrganizationId { get; set; }

        public double Distance { get; set; }
    }
}
