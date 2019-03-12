using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.Cors
{
    public static class CorsPolicyServiceCollectionExtension
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services, DondeCorsPolicy corsPolicy)
        {
            services.AddCors(o => o.AddPolicy(DondeCorsPolicy.CorsPolicyKey, builder =>
            {
                builder.WithOrigins(BuildPolicy(corsPolicy.AllowedOrigins))
                       .WithMethods(BuildPolicy(corsPolicy.AllowedMethods))
                       .WithHeaders(BuildPolicy(corsPolicy.AllowedHeaders));
            }));
        }

        private static string BuildPolicy(List<string> policyTypeList)
        {
            policyTypeList = policyTypeList ?? new List<string> { DondeCorsPolicy.AllowAllKey };

            return string.Join(",", policyTypeList);
        }
    }
}
