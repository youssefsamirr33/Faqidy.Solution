using AutoMapper;
using Faqidy.Application.Common;
using Faqidy.Application.Exceptions;
using Faqidy.Application.SocialMedia.Posts.Dtos;
using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Specification.Posts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.Posts.Command
{
    public record AddPostCommand(PostDto model) : IRequest<Result<PostReponseDto>>;
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Result<PostReponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPostCommandHandler> _logger;

        public AddPostCommandHandler(IUnitOfWork unitOfWork , IMapper mapper , ILogger<AddPostCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<PostReponseDto>> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("start creating a post.");
            var _repo = _unitOfWork.GetRepository<SocialPost, Guid>();

            var postMapper = _mapper.Map<SocialPost>(request.model);
            postMapper.CreateOn = DateTime.UtcNow;
            postMapper.CreatedBy = request.model.USerId;
            postMapper.LastModifiedBy = request.model.USerId;
            postMapper.LastModifiedOn = DateTime.UtcNow;

            await _repo.AddAsync(postMapper);

            var result =  await _unitOfWork.CompleteAsync(cancellationToken);
            if (result == 0)
            {
                _logger.LogError("Error when create a new post");
                throw new BadRequestException("Faild to Create a post , please Try again");
            }

            var spec = new PostWithUserSpecifications(postMapper.Id);
            var postResponse = await _repo.GetWithSpecAsync(spec);

            var postMapperResponse = _mapper.Map<PostReponseDto>(postResponse);
            _logger.LogInformation("Create a new post successfully");
            return Result<PostReponseDto>.Success(postMapperResponse);

        }
    }
}
