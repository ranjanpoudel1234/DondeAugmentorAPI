using Donde.Augmentor.Core.Domain.Enum;
using System;

namespace Donde.Augmentor.Web.ViewModels.V2.Organization
{
    public class OrganizationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public AddressViewModel Address { get; set; }
        public OrganizationLogoMetadataViewModel Logo { get; set; }
        public OrganizationTypes Type { get; set; }
    }
}
