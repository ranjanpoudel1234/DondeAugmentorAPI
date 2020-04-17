using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Interfaces;
using System;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class AugmentObjectMedia : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }

        public Guid? AvatarId { get; set; }
        public Guid? AudioId { get; set; }
        public Guid? VideoId { get; set; }

        public AugmentObjectMediaTypes MediaType { get; set; }
        public Guid AugmentObjectId { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Audio Audio { get; set; }
        public Video Video { get; set; }
        public Avatar Avatar { get; set; }
        public AugmentObject AugmentObject { get; set; }
    }
}
