using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels.V2.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public List<Guid> OrganizationIds { get; set; }

        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public bool IsDeleted { get; set; }

        public string EmailAddress { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
