//using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using Swashbuckle.Swagger;
//using Microsoft.AspNet.OData;
//using Microsoft.OpenApi.Models;

//namespace Donde.Augmentor.Web.Swagger
//{
//    public class CustomDocumentFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
//    {
//        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//        {
//            Assembly assembly = typeof(ODataController).Assembly;
//            var thisAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes().ToList();
//            var odatacontrollers = thisAssemblyTypes.Where(t => t.BaseType == typeof(Microsoft.AspNet.OData.ODataController)).ToList();
//            var odatamethods = new[] { "Get", "Put", "Post", "Delete" };

//            foreach (var odataContoller in odatacontrollers)  // this the OData controllers in the API
//            {
//                var methods = odataContoller.GetMethods().Where(a => odatamethods.Contains(a.Name)).ToList();
//                if (!methods.Any())
//                    continue; // next controller 

//                foreach (var method in methods)  // this is all of the methods in controller (e.g. GET, POST, PUT, etc)
//                {
//                    StringBuilder sb = new StringBuilder();
//                    sb.Append("/" + method.Name + "(");
//                    List<String> listParams = new List<String>();
//                    var parameterInfo = method.GetParameters();
//                    foreach (ParameterInfo pi in parameterInfo)
//                    {
//                        listParams.Add(pi.ParameterType + " " + pi.Name);
//                    }
//                    sb.Append(String.Join(", ", listParams.ToArray()));
//                    sb.Append(")");
//                    var path = "/" + "api" + "/" + odataContoller.Name.Replace("Controller", "") + sb.ToString();
//                    var odataPathItem = new OpenApiPathItem();
//                    var op = new Operation();

//                    // The odata methods will be listed under a heading called OData in the swagger doc
//                    op.tags = new List<string> { "OData" };
//                    op.operationId = "OData_" + odataContoller.Name.Replace("Controller", "");

//                     //This hard-coded for now, set it to use XML comments if you want
//                    op.summary = "Summary about method / data";
//                    op.description = "Description / options for the call.";

//                    op.consumes = new List<string>();
//                    op.produces = new List<string> { "application/atom+xml", "application/json", "text/json", "application/xml", "text/xml" };
//                    op.deprecated = false;

//                    var response = new Response() { description = "OK" };
//                    response.schema = new Schema { type = "array", items = context.SchemaRegistry.GetOrRegister(method.ReturnType) };
//                    op.responses = new Dictionary<string, Response> { { "200", response } };

//                    var security = GetSecurityForOperation(odataContoller);
//                    if (security != null)
//                        op.security = new List<IDictionary<string, IEnumerable<string>>> { security };

//                    odataPathItem.Get = op;

//                    try
//                    {
//                        swaggerDoc.Paths.Add(path, odataPathItem);
//                    }
//                    catch { }
//                }
//            }
//        }

//        private Dictionary<string, IEnumerable<string>> GetSecurityForOperation(MemberInfo odataContoller)
//        {
//            Dictionary<string, IEnumerable<string>> securityEntries = null;
//            if (odataContoller.GetCustomAttribute(typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute)) != null)
//            {
//                securityEntries = new Dictionary<string, IEnumerable<string>> { { "oauth2", new[] { "actioncenter" } } };
//            }
//            return securityEntries;
//        }
//    }
//}
