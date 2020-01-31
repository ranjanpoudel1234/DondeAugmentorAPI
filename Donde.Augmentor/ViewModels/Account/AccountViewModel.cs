using System;

namespace Donde.Augmentor.Web.ViewModels.Account
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
