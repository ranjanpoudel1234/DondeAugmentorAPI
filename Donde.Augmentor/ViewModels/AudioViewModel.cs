using System;

namespace Donde.Augmentor.Web.ViewModels
{
    public class AudioViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Guid FileId { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
    }
}
