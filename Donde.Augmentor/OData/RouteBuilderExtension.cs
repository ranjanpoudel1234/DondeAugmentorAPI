using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace Donde.Augmentor.Web.OData
{
    public static class RouteBuilderExtensions
    {
        public static IRouteBuilder BuildDondeOData
            (this IRouteBuilder builder,
            VersionedODataModelBuilder modelBuilder)
        {
            //this allows json response to be camecase.
            modelBuilder.ModelBuilderFactory = () => new ODataConventionModelBuilder().EnableLowerCamelCase();
        
            builder.Select().Filter().OrderBy().MaxTop(ODataConstants.MaximumTopAllowed).Count();
            builder.MapVersionedODataRoutes("odata", "api/v{apiVersion}", modelBuilder.GetEdmModels());

            builder.EnableDependencyInjection();

            return builder;
        }
    }
}
