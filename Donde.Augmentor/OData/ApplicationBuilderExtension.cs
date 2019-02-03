using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;

namespace Donde.Augmentor.Web.OData
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseAionOdata(this IApplicationBuilder app)
        {
            var modelBuilder = app.ApplicationServices.GetService(typeof(VersionedODataModelBuilder)) as VersionedODataModelBuilder;
            app.UseMvc(builder => builder.BuildAionOData(modelBuilder));
            return app;
        }
    }
}
