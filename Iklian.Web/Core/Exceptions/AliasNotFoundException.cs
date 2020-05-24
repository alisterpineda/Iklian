using Microsoft.AspNetCore.Http;

namespace Iklian.Web.Core.Exceptions
{
    public class AliasNotFoundException : IklianWebException
    {
        public AliasNotFoundException() : base(StatusCodes.Status404NotFound)
        {

        }
    }
}
