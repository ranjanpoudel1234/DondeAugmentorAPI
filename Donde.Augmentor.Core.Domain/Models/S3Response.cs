using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class S3Response
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
