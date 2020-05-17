using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Web.ViewModels.AugmentObject;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels
{
    /// <summary>
    /// Improvement, the get queries do not load augmentObjectMedias and augmentObjectLocations right now.
    /// to get the augmentObjects first and then to hydrate media and location information if needed.
    /// 
    /// Ideally:(doable if have time soon)
    /// Get should load augmentObjectLocations and augmentObjectMedias with hydration as talked above.
    /// Post should load all the hydrated properties.
    /// 
    /// MIGHT ADD V2 ENDPOINTS WITH BETTER PAYLOAD WITH MULTIPLE SUPPORT(and payload consitency 
    /// between GETALL and POST, also NetTopologyUse potentially) AND JUST USE THAT/UPDATE IN WEB.
    /// CAUSE OVERARCHITECTURE IS ALSO A THING. So overly worrying about this might hurt more
    /// than helping in future.
    /// 
    /// Not that important IMO:
    /// AvatarId, name, url, ImagerId, name, url, VideoId, name, url should MAY BE(odata filter) all move under AugmentObjectMedias(this requires mobile change,
    /// this also requires changing that query to not join at first).
    /// 
    /// LATER ON VERY IMPORTANT :
    /// Really important if one augmentObject has more than one media later on, 
    /// we dont want to repeat them. Its ok to repeat for locations so join on location is just fine.
    /// if this happens, it will have to be v2 endpoints with possible mobile updates(may be not, since join will be handled on backend).
    /// </summary>
    public class AugmentObjectViewModel : IAugmentObjectViewModel
    {
        public Guid Id { get; set; }
        public Guid AugmentImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public AugmentObjectTypes Type { get; set; }
        public Guid OrganizationId { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public AugmentObjectMediaTypes MediaType { get; set; }
        public Guid MediaId { get; set; }
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
        public string OriginalSizeImageUrl { get; set; }
        public string ImageUrl { get; set; }

        public List<AugmentObjectMediaViewModel> AugmentObjectMedias { get; set; }

        public List<AugmentObjectLocationViewModel> AugmentObjectLocations { get; set; }
        public double? Distance { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
