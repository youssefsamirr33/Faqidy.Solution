using Faqidy.APIs.Errors;
using Faqidy.Application.Exceptions;
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
            throw new NotFoundException();
        }

        [HttpGet("bad-request")]
        public ActionResult BadRequestError()
        {
            throw new BadRequestException();
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
