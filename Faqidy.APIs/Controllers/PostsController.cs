using Faqidy.Application.Common;
using Faqidy.Application.SocialMedia.Posts.Command;
using Faqidy.Application.SocialMedia.Posts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Faqidy.APIs.Controllers
{
    public class PostsController(ISender _sender) : BaseController
    {

        [HttpPost("new-post")]
        public async Task<ActionResult<Result<PostReponseDto>>> CreatePost(PostDto postReponse)
        {
            var response = await _sender.Send(new AddPostCommand(postReponse));
            return Ok(response);
        }
    }
}
