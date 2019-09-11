using System;

namespace Donde.Augmentor.Core.Domain.Dto
{
    public class MediaAttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string MimeType { get; set; }
    }
}
