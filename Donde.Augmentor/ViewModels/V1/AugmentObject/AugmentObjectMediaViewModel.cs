using Donde.Augmentor.Core.Domain.Enum;
using System;

namespace Donde.Augmentor.Web.ViewModels.V1.AugmentObject
{
    public class AugmentObjectMediaViewModel
    {
        public Guid Id { get; set; }

        public Guid? AvatarId { get; set; }
        public Guid? AudioId { get; set; }
        public Guid? VideoId { get; set; }

        public AugmentObjectMediaTypes MediaType { get; set; }
        public Guid AugmentObjectId { get; set; }
    }
}
