using System.Collections.Generic;

namespace Donde.Augmentor.Core.Domain
{
    public class DomainConstants
    {
        public class DondeErrorMessages
        {
            public const string INVALID_IMAGE_FILE_TYPE = "INVALID_IMAGE_FILE_TYPE";
            public const string INVALID_VIDEO_FILE_TYPE = "INVALID_VIDEO_FILE_TYPE";
            public const string INVALID_AUDIO_FILE_TYPE = "INVALID_AUDIO_FILE_TYPE";
            public const string PROPERTY_EMPTY = "PROPERTY_EMPTY";
            public const string INVALID_VALUE = "INVALID_VALUE";
        }

        public class ValidMediaExtensions
        {
            public static List<string> VALID_IMAGE_EXTENSIONS = new List<string> { ".png",".jpg" };

            public static List<string> VALID_VIDEO_EXTENSIONS = new List<string> { ".mp4"};

            public static List<string> VALID_AUDIO_EXTENSIONS = new List<string> { ".mp3", ".wav" };
        }
    }
}
