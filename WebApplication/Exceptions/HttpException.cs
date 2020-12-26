using System;
using System.Net;

namespace WebApplication.Exceptions
{
    public class HttpException: Exception
    {
        public HttpException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }

        public HttpStatusCode Code { get; }
        public object Errors { get; }
    }
}