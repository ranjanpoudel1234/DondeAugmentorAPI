using Donde.Augmentor.Core.Domain.Models;
using System;

namespace Donde.Augmentor.Core.Domain.Interfaces
{
    public interface IMetricModel
    {
         Guid AugmentObjectId { get; set; }
         AugmentObject AugmentObject { get; set; }
         string DeviceUniqueId { get; set; }
         string DeviceName { get; set; }
         string DeviceId { get; set; }
    }
}
