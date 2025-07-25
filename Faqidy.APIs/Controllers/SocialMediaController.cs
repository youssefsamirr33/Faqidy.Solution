using Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faqidy.APIs.Controllers
{
    public class SocialMediaController : BaseController
    {
        private readonly IMediator _mediator;

        public SocialMediaController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("NewMissingProfile")]
        public async Task<ActionResult<object>> CreateNewMissingProfile(AddMissingProfileCommands command)
        {
            var sender = await _mediator.Send(command);
            return Ok(sender);
        }
    }
}
