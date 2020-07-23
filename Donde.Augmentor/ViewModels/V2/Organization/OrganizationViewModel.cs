using Donde.Augmentor.Core.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels.V2.Organization
{
    public class OrganizationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public AddressViewModel Address { get; set; }
        public OrganizationLogoViewModel Logo { get; set; }
        public OrganizationTypes Type { get; set; }
    }
}
