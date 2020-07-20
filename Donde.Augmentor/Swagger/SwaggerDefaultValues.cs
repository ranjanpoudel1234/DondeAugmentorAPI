//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;

//namespace Donde.Augmentor.Web.Swagger
//{
//    public class SwaggerDefaultValues : IParameterFilter
//    {
//        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
//        {
//            if (!(parameter is NonBodyParameter nonBodyParameter))
//                return;

//            if (nonBodyParameter.Description == null)
//            {
//                nonBodyParameter.Description = context.ApiParameterDescription.ModelMetadata?.Description;
//            }

//            if (context.ApiParameterDescription.RouteInfo == null)
//                return;

//            if (nonBodyParameter.Default == null)
//            {
//                nonBodyParameter.Default = context.ApiParameterDescription.RouteInfo.DefaultValue;
//            }

//            parameter.Required |= !context.ApiParameterDescription.RouteInfo.IsOptional;
//        }
//    }
//}
