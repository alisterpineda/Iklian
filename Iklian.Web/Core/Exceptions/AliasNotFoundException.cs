using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Iklian.Web.Core.Exceptions
{
    public class AliasNotFoundException : BaseException
    {
        public AliasNotFoundException() : base(StatusCodes.Status404NotFound)
        {

        }
    }
}
