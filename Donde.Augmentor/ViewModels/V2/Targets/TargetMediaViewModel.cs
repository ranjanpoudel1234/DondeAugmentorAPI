using Donde.Augmentor.Core.Domain.Enum;
using System;

namespace Donde.Augmentor.Web.ViewModels.V2.Targets
{
    public class TargetMediaViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AugmentObjectMediaTypes Type { get; set; }
        public string Url { get; set; }
    }
}
