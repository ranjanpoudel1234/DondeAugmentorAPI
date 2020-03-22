using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Buffers;
using System.Reflection;

namespace Donde.Augmentor.Web.Attributes
{
    public class IgnoreJsonIgnore
    {
        public class IgnoreJsonIgnoreAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
            {
                if (actionExecutedContext.Result is ObjectResult objectResult)
                {
                    objectResult.Formatters.Add(new JsonOutputFormatter(
                        new JsonSerializerSettings
                        {
                            ContractResolver = new IgnoreJsonIgnoreContractResolver(),
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        },
                        actionExecutedContext.HttpContext.RequestServices.GetRequiredService<ArrayPool<char>>()));
                }
            }
        }

        public class IgnoreJsonIgnoreContractResolver : CamelCasePropertyNamesContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);
                property.Ignored = false;
                return property;
            }
        }
    }
}
