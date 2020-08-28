using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.ViewModels.V1
{
    public class VideoViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Guid FileId { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
    }
}
