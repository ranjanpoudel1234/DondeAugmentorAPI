using System.Collections.Generic;

namespace Donde.Augmentor.Web.Cors
{
    public class DondeCorsPolicy
    {
        public const string CorsPolicyKey = "donde-cors-policy";
        public const string AllowAllKey = "*";

        public List<string> AllowedOrigins { get; set; }

        public List<string> AllowedMethods { get; set; }

        public List<string> AllowedHeaders { get; set; }
    }
}
