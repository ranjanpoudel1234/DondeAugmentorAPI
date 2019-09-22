using Microsoft.AspNetCore.Mvc.Filters;

namespace Donde.Augmentor.Web.Filters
{
    public interface IExceptionFilter : IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
