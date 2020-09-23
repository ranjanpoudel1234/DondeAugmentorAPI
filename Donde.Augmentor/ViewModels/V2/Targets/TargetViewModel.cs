using Donde.Augmentor.Core.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels.V2.Targets
{
    public class TargetViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public AugmentObjectTypes Type { get; set; }
        public Guid OrganizationId { get; set; }

        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public bool IsDeleted { get; set; }

        public TargetImageViewModel Image { get; set; }
        public TargetMediaViewModel Media { get; set; }
        public TargetAvatarViewModel Avatar { get; set; }
        public List<TargetLocationViewModel> Locations { get; set; }
    }
}

