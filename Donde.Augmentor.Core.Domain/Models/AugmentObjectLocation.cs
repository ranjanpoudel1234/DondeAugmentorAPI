using System;
using Donde.Augmentor.Core.Domain.Interfaces;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class AugmentObjectLocation : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public Guid AugmentObjectId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public AugmentObject AugmentObject { get; set; }
    }
}
