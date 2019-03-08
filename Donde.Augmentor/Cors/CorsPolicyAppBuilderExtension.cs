using Microsoft.AspNetCore.Builder;

namespace Donde.Augmentor.Web.Cors
{
    public static class CorsPolicyAppBuilderExtension
    {
        public static void UseSpokenPastCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(DondeCorsPolicy.CorsPolicyKey);
        }
    }
}
