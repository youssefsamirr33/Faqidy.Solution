
using System.Text.Json;

namespace Faqidy.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode , string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageByStatusCode(statusCode);
        }

        private string GetMessageByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                // Success
                200 => "OK",
                201 => "Created",
                204 => "No Content",

                // Redirection
                301 => "Moved Permanently",
                304 => "Not Modified",

                // Client Errors
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                408 => "Request Timeout",
                429 => "Too Many Requests",

                // Server Errors
                500 => "Internal Server Error",
                501 => "Not Implemented",
                503 => "Service Unavailable",
                504 => "Gateway Timeout",

                // Default for any unhandled codes
                _ => "Unknown Status"
            };
        }

      
    }
}
