using System;

namespace Donde.Augmentor.Web.ViewModels
{
    public class OrganizationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
    }
}
