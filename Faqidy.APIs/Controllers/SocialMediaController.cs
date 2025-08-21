using Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile;
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
        private readonly ApplicationDbContext _context;

        public SocialMediaController(IMediator mediator , ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }


        [HttpPost("NewMissingProfile")]
        public async Task<ActionResult<object>> CreateNewMissingProfile([FromForm]AddMissingProfileCommands command)
        {
            var sender = await _mediator.Send(command);
            return Ok(sender);
        }

        [HttpGet("get-missing-childs")]
        public async Task<ActionResult<object>> GetMissingChildProfile()
        {
            var result = await _context.Set<MissingChild>().ToListAsync();
            return Ok(result);
        }
    }
}
