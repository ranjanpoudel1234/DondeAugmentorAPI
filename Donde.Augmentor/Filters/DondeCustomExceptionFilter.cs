using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Donde.Augmentor.Web.Filters
{
    public class DondeCustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public DondeCustomExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("CustomExceptionFilter");
        }

        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception;
            switch (exceptionType)
            {
                case HttpNotFoundException _:
                    context.Result = ProcessException(context, HttpStatusCode.NotFound, ErrorMessages.ObjectNotFound);
                    break;
                case NullReferenceException _:
                    context.Result = ProcessException(context, HttpStatusCode.NotFound, ErrorMessages.ObjectNotFound);
                    break;
                case UnauthorizedAccessException _:
                    context.Result = ProcessException(context, HttpStatusCode.Unauthorized);
                    break;
                case HttpBadRequestException _:
                    context.Result = ProcessException(context, HttpStatusCode.BadRequest, ErrorMessages.BadRequest);
                    break;
                default:
                    context.Result =
                        ProcessException(context, HttpStatusCode.InternalServerError, "Internal Server Error Occurred.");
                    break;
            }

            base.OnException(context);
        }

        private ObjectResult ProcessException(
            ExceptionContext context,
            HttpStatusCode httpStatusCode,
            string message = null)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];
            var exception = context.Exception;

            var errorMessage = $"{httpStatusCode} occurred while executing action {action} inside {controller} controller.";
            _logger.LogError("{errorMessage}. Exception: {@exception}", errorMessage, exception);

            var errorViewModel = new ErrorResponseViewModel()
            {
                Message = message ?? exception.ToString(),
                Code = (int)httpStatusCode,
                Success = false
            };

            var objectResult = new ObjectResult(string.Empty)
            {
                Value = errorViewModel,
                StatusCode = (int)httpStatusCode
            };

            return objectResult;
        }
    }
}
