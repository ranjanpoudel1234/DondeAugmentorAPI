using Donde.Augmentor.Core.Domain.Interfaces;
using System;

namespace Donde.Augmentor.Core.Domain.Models.Metrics
{
    public class AugmentObjectVisitMetric : IDondeModel, IMetricModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }

        public Guid AugmentObjectId { get; set; }
        public AugmentObject AugmentObject { get; set; }

        public string DeviceUniqueId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
