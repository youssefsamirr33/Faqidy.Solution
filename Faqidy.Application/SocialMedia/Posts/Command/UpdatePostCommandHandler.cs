using AutoMapper;
using Faqidy.Application.Common;
using Faqidy.Application.Exceptions;
using Faqidy.Application.SocialMedia.Posts.Dtos;
using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Enums;
using Faqidy.Domain.Specification.Posts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.Posts.Command
{
    public record UpdatePostCommand(
        Guid Id,
        UpdatePostDto model
        ) : IRequest<Result<PostReponseDto>>;
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result<PostReponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdatePostCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler( IUnitOfWork unitOfWork , ILogger<UpdatePostCommandHandler> logger , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<PostReponseDto>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"start updated post with id {request.Id}");

                var _repo = _unitOfWork.GetRepository<SocialPost, Guid>();

                var spec = new PostWithUserSpecifications(request.Id);
                var post = await _repo.GetWithSpecAsync(spec);

                if (post is null)
                    throw new NotFoundException($"This post with Id : {request.Id} is not founded.");

                post.Title = request.model.title;
                post.Content = request.model.content;
                post.PostType = request.model.postType;
                post.LastModifiedOn = DateTime.UtcNow;

                _repo.Update(post);

                var updatedModel = await _unitOfWork.CompleteAsync(cancellationToken);
                if (updatedModel == 0)
                    throw new BadRequestException($"Failed Updated post with id: {request.Id} , please Try again");
                _logger.LogInformation($"updated post with id {request.Id} is successfully");
                var mappedModel = _mapper.Map<PostReponseDto>(post);

                return Result<PostReponseDto>.Success(mappedModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed updated post with id {request.Id}" , ex);
                return Result<PostReponseDto>.Failure(ex.Message);
            }
        }
    }
}
