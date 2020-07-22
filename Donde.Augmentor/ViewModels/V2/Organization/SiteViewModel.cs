using Donde.Augmentor.Core.Domain.Enum;
using System;

namespace Donde.Augmentor.Web.ViewModels.V2.Organization
{
    public class SiteViewModel
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set;}
        public SiteTypes Type { get; set; }

        public SiteLocationViewModel Location { get; set; }
    }
}
