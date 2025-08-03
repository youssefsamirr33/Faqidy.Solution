using System.Text.Json;

namespace Faqidy.APIs.Errors
{
    public class ExeptionHandlerErrorResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ExeptionHandlerErrorResponse(int statusCode , string? message = null , string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
