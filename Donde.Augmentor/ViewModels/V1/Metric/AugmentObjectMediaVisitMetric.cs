using System;

namespace Donde.Augmentor.Web.ViewModels.V1.Metric
{
    public class AugmentObjectMediaVisitMetricViewModel
    {
        public Guid Id {get;set;}
        public Guid AugmentObjectId { get; set; }
        public Guid AugmentObjectMediaId { get; set; }
        public string DeviceUniqueId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
    }
}
