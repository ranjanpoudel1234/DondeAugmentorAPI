using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1;
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
                    PositionX = 0,
                    PositionY = -7,
                    PositionZ = 0
                },
                Scale = new AvatarScale
                {
                    ScaleX = 0.022,
                    ScaleY = 0.022,
                    ScaleZ = 0.022
                },
                Animation = new AvatarAnimation
                {
                    Name="Armature|ArmatureAction.001",
                    Run = true,
                    Loop = true,
                    Delay = 1000,
                    Duration = 8000//in milliseconds, can make it fast and slow.
                },
                Rotation = new AvatarRotation
                {
                    RotationX = 0,
                    RotationY = 0,
                    RotationZ = 0
                }
            };

            var configurationSerizalized = JsonConvert.SerializeObject(avatarConfiguration);

            configurationSerizalized.ShouldNotBeNull();
        }
    }
}
