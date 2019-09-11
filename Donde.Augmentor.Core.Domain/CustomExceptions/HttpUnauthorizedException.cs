using System;

namespace Donde.Augmentor.Core.Domain.CustomExceptions
{
    public class HttpUnauthorizedException : Exception
    {
        public HttpUnauthorizedException()
        { }

        public HttpUnauthorizedException(string message)
            : base(message)
        { }

        public HttpUnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
