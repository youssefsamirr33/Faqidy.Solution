using Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile;
using Faqidy.Application.SocialMedia.MissingProfile.Queries;
using Faqidy.Domain.Entities.SotialMediaModule;
using Faqidy.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<object>> CreateNewMissingProfile([FromForm]AddMissingProfileCommands command)
        {
            var sender = await _mediator.Send(command);
            return Ok(sender);
        }

        [HttpGet("Get-childs-profiles")]
        public async Task<ActionResult<object>> GetMissingChildProfile([FromQuery]GetMissingProfileQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
