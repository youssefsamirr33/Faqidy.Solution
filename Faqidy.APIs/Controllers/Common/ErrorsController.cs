using Faqidy.APIs.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Faqidy.APIs.Controllers.Common
{
    [Route("Errors")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet("{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == (int)HttpStatusCode.NotFound)
                return NotFound(new ApiResponse(statusCode, "The Requested endpoint Not found."));
            return StatusCode(statusCode);
        }
    }
}
