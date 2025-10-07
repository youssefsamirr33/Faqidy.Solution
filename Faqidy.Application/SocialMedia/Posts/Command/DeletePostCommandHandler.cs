using Faqidy.Application.Common;
using Faqidy.Application.Exceptions;
using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.sotialMediaModule;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.Posts.Command
{
    public record DeletePostCommand(Guid Id) : IRequest<Result<string>>;
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeletePostCommandHandler> _logger;

        public DeletePostCommandHandler(IUnitOfWork unitOfWork , ILogger<DeletePostCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start Deleted post with Id {request.Id}");
                var _repo = _unitOfWork.GetRepository<SocialPost, Guid>();

                var post = await _repo.GetAsync(request.Id);
                if (post is null)
                    throw new NotFoundException($"Not found post with Id : {request.Id}");
                _repo.Delete(post);
                var deletedPost = await _unitOfWork.CompleteAsync(cancellationToken);
                if (deletedPost == 0)
                    throw new BadRequestException($"Faild when deleted post with Id {request.Id} , Please Try again.");
                _logger.LogInformation($"Deleted post with Id '{request.Id}' successfully.");

                return Result<string>.Success($"Deleted post with Id '{request.Id}' successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Faild when deleted post with Id {request.Id} , Please Try again.");
                return Result<string>.Failure(ex.Message);
            }
        }
    }
}
