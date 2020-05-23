using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Iklian.Web.Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}
