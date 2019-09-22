using System;

namespace Donde.Augmentor.Core.Domain.CustomExceptions
{
    public class HttpBadRequestException : Exception
    {
        public HttpBadRequestException()
        { }

        public HttpBadRequestException(string message)
            : base(message)
        { }

        public HttpBadRequestException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
