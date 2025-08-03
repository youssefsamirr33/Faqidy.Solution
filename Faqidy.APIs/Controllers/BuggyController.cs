using Faqidy.APIs.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faqidy.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet("not-found")]
        public ActionResult NotFoundError()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("bad-request")]
        public ActionResult BadRequestError()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("validation-error/{id}")]
        public ActionResult ValidationError(int id)
        {
            return BadRequest();
        }

        [HttpGet("server-error")]
        public ActionResult ServerError()
        {
            throw new Exception();
        }
    }
}
