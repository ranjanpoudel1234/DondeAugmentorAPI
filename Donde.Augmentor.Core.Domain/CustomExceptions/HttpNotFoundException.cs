using System;

namespace Donde.Augmentor.Core.Domain.CustomExceptions
{
    public class HttpNotFoundException : Exception
    {
        public HttpNotFoundException()
        { }

        public HttpNotFoundException(string message)
            : base(message)
        { }

        public HttpNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
