using System;

namespace Donde.Augmentor.Web.ViewModels.V1.Metric
{
    public class AugmentObjectVisitMetricViewModel
    {
        public Guid Id { get; set; }
        public Guid AugmentObjectId { get; set; }
        public string DeviceUniqueId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
    }
}
