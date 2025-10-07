using Faqidy.Application.Common;
using Faqidy.Application.SocialMedia.Posts.Command;
using Faqidy.Application.SocialMedia.Posts.Dtos;
using Faqidy.Application.SocialMedia.Posts.Queries;
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

        [HttpPut("update-post/{Id}")]
        public async Task<ActionResult<Result<PostReponseDto>>> UpdatePost(Guid Id, [FromBody] UpdatePostDto model)
        {
            var response = await _sender.Send(new UpdatePostCommand(Id, model));
            return Ok(response);
        }

        [HttpDelete("delete-post/{Id}")]
        public async Task<ActionResult<Result<string>>> DeletePost(Guid Id)
        {
            var response = await _sender.Send(new DeletePostCommand(Id));
            return Ok(response);
        }

        [HttpGet("get-post/{postId}")]
        public async Task<ActionResult<Result<PostToReturnSpecDataDto>>> GetSpecPost(Guid postId)
        {
            var response = await _sender.Send(new GetSpecificPostQuery(postId));
            return Ok(response);
            // 93b90571-d5c9-403f-3898-08ddf3e04f04
        }
    }
}
