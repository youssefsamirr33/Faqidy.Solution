using AutoMapper;
using Faqidy.Application.Common;
using Faqidy.Application.Exceptions;
using Faqidy.Application.SocialMedia.Posts.Dtos;
using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Specification.Posts;
using Faqidy.Infrastructure.Persistance.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.Posts.Queries
{
    public record GetSpecificPostQuery(Guid PostId) : IRequest<Result<PostToReturnSpecDataDto>>;
    public class GetSpecificPostQueryHandler(
        ILogger<GetSpecificPostQueryHandler> _logger,
        IMapper _mapper,
        IUnitOfWork _unitOfWork,
        ApplicationDbContext context
        ) : IRequestHandler<GetSpecificPostQuery, Result<PostToReturnSpecDataDto>>
    {
        
        public async Task<Result<PostToReturnSpecDataDto>> Handle(GetSpecificPostQuery request, CancellationToken cancellationToken)
        {
            var spec = new PostWithUserAndMissingChildAndMissingPhotoSpecifications(request.PostId);
            var post = await _unitOfWork.GetRepository<SocialPost , Guid>().GetWithSpecAsync(spec);


            if (post is null)
                throw new NotFoundException(nameof(SocialPost), request.PostId);

            // count of likes and comments 
            
            var countLikes = await _unitOfWork.GetRepository<PostInterActive, Guid>().GetCountForlikesOrComments(request.PostId);
            var countComments = await _unitOfWork.GetRepository<Comment, Guid>().GetCountForlikesOrComments(request.PostId);

            var mappedPost = _mapper.Map<PostToReturnSpecDataDto>(post);
            mappedPost.CountOfLikes = countLikes;
            mappedPost.CountOfComments = countComments;

            return Result<PostToReturnSpecDataDto>.Success(mappedPost);


        }
    }
}
