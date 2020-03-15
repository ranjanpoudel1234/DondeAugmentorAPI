namespace Donde.Augmentor.Web.ViewModels
{
    public class AvatarConfigurationViewModel
    {
        public AvatarPosition Position { get; set; }
        public AvatarScale Scale { get; set; }
        public AvatarAnimation Animation { get; set; }
        public AvatarRotation Rotation { get; set; }
    }

    public class AvatarPosition
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
    }

    public class AvatarScale
    {
        public int ScaleX { get; set; }
        public int ScaleY { get; set; }
        public int ScaleZ { get; set; }
    }

    public class AvatarAnimation
    {
        public string Name { get; set; }
        public bool Run { get; set; }
        public bool Loop { get; set; }
        public int Delay { get; set; }
        public int Duration { get; set; }
    }

    public class AvatarRotation
    {
        public int RotationX { get; set; }
        public int RotationY { get; set; }
        public int RotationZ { get; set; }
    }
}
