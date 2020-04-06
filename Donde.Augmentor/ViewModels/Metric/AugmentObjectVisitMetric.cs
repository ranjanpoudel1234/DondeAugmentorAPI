using System;

namespace Donde.Augmentor.Core.Domain.Models.Metrics
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
