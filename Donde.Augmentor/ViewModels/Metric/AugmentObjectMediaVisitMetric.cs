using System;

namespace Donde.Augmentor.Core.Domain.Models.Metrics
{
    public class AugmentObjectMediaVisitMetricViewModel
    {
        public Guid Id {get;set;}
        public Guid AugmentObjectId { get; set; }
        public Guid AugmentObjectMediaId { get; set; }
        string DeviceUniqueId { get; set; }
        string DeviceName { get; set; }
        string DeviceId { get; set; }
    }
}
