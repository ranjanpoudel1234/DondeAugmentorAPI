using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Models;
using System;

namespace Donde.Augmentor.Core.Domain.Dto
{
    /// <summary>
    /// This class stores any extra information we want to transfer along with augment main object.
    /// </summary>
    public class AugmentObjectDto : AugmentObject
    {
        public AugmentObjectMediaTypes MediaType { get; set; }
        public Guid? AvatarId { get; set; }
        public string AvatarName { get; set; }
        public string AvatarUrl { get; set; }
        public string AvatarConfiguration { get; set; }
        public Guid? AudioId { get; set; }
        public string AudioName { get; set; }
        public string AudioUrl { get; set; }
        public Guid? VideoId { get; set; }
        public string VideoName { get; set; }
        public string VideoUrl { get; set; }

        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }

    public class GeographicalAugmentObjectDto: AugmentObjectDto
    {
        public double Distance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
