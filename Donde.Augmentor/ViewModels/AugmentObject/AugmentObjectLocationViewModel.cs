using System;

namespace Donde.Augmentor.Web.ViewModels.AugmentObject
{
    public class AugmentObjectLocationViewModel
    {
        public Guid Id { get; set; }
        public Guid AugmentObjectId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
