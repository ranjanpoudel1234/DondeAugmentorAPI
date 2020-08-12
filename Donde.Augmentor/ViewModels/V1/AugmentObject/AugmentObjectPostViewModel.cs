using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Web.ViewModels.V1.AugmentObject;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels.V1.AugmentObject
{
    public class AugmentObjectPostViewModel
    {
        public Guid Id { get; set; }
        public Guid AugmentImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public AugmentObjectTypes Type { get; set; }
        public Guid OrganizationId { get; set; }
        public AugmentObjectMediaViewModel AugmentObjectMedia { get; set; }

        public List<AugmentObjectLocationViewModel> AugmentObjectLocations { get; set; }
    }
}

