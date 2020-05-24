namespace Iklian.Web.Areas.Api.Models
{
    public class ErrorResponse : OperationResponse
    {
        public ErrorResponse(string message)
        {
            Success = false;
            Message = message;
        }

        public string Message { get; }
    }
}
