namespace Donde.Augmentor.Core.Domain
{
    public class DomainSettings
    {
        public UploadSettings UploadSettings { get; set; }

        public MediaSettings MediaSettings { get; set; }

        public GeneralSettings GeneralSettings { get; set; }
    }

    public class UploadSettings
    {
        public string ServerTempUploadFolderName { get; set; }
        public string BucketName { get; set; }
        public string ImageFolderName { get; set; }
        public string VideosFolderName { get; set; }
        public int UploadPartSizeInMB { get; set; }
    }

    public class MediaSettings
    {
        public int ImageResizeWidth { get; set; }
        public int ImageResizeHeight { get; set; }
        public int ImageQuality { get; set; }
    }

    public class GeneralSettings
    {
        public string StorageBasePath { get; set; }
    }
}
