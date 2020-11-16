using System;

namespace Donde.Augmentor.Web.ViewModels.V2.Metric
{
    public class TargetMediaVisitMetricViewModel
    {
        public Guid Id {get;set;}
        public Guid AugmentObjectId { get; set; }
        public Guid MediaId { get; set; }
        public string DeviceUniqueId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
    }
}
