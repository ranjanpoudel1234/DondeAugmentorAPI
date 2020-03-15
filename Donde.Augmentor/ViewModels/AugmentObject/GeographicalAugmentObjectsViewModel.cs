using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Web.ViewModels.AugmentObject;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels
{
    /// <summary>
    /// Improvement, ideally this should be similar to post with AugmentObjectMedia and AugmentObjectLocation being
    /// the children properties. For that to happen, we will have to update the get queries(which will also be an improvement)
    /// to get the augmentObjects first and then to hydrate media and location information if needed.
    /// </summary>
    public class GeographicalAugmentObjectsViewModel : IAugmentObjectViewModel
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
        public string AvatarConfigurationString { get; set; }
        public AvatarConfigurationViewModel AvatarConfiguration { get; set; }
        public Guid? AudioId { get; set; }
        public string AudioName { get; set; }
        public string AudioUrl { get; set; }
        public Guid? VideoId { get; set; }
        public string VideoName { get; set; }
        public string VideoUrl { get; set; }

        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

        public List<AugmentObjectMediaViewModel> AugmentObjectMedias { get; set; }

        public List<AugmentObjectLocationViewModel> AugmentObjectLocations { get; set; }

        public double Distance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
