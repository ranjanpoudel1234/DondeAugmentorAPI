using System;

namespace Donde.Augmentor.Web.ViewModels.V2.Targets
{
    public class TargetAvatarViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ConfigurationString { get; set; }
        public AvatarConfigurationViewModel Configuration { get; set; }
    }
}
