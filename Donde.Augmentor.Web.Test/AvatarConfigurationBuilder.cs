using Donde.Augmentor.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Shouldly;

namespace Donde.Augmentor.Web.Test
{
    [TestClass]
    public class AvatarConfigurationBuilder
    {
        [TestMethod]
        public void BuildAvatarConfiguration_WithProvidedConfig_BuildsConfiguration()
        {
            var avatarConfiguration = new AvatarConfigurationViewModel
            {
                Position = new AvatarPosition
                {
                    PositionX = 2,
                    PositionY = -10,
                    PositionZ = -300
                }
            };

            var configurationSerizalized = JsonConvert.SerializeObject(avatarConfiguration);

            configurationSerizalized.ShouldNotBeNull();
        }
    }
}
