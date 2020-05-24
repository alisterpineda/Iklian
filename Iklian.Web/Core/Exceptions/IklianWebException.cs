using System;

namespace Iklian.Web.Core.Exceptions
{
    public abstract class IklianWebException : Exception
    {
        protected IklianWebException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}
