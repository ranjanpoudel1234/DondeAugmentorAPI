using Donde.Augmentor.Web.Cors;
using System.Collections.Generic;

namespace Donde.Augmentor.Web
{
    public class AppSetting
    {
        public Host Host { get; set; }
    }

    public class Host
    {
        public DondeCorsPolicy CorsPolicy { get; set; }

        public string APIEndPointUrl { get; set; }

        public string UIEndPointUrl { get; set; }
    }
}
