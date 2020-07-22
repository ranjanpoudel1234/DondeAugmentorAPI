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
        public string EmailAddress { get; set; }
        public AddressViewModel Address { get; set; }
        public string LogoName { get; set; }
        public string LogoUrl { get; set; }
        public string LogoMimeType { get; set; }
        public OrganizationTypes Type { get; set; }
        public List<SiteViewModel> Sites { get; set; }
    }
}
