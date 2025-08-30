using AutoMapper;
using Faqidy.Application.SocialMedia.MissingProfile.DTOs;
using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.SotialMediaModule;
using Faqidy.Domain.Specification.Child_profile;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Faqidy.Application.SocialMedia.MissingProfile.Queries
{
    public record GetMissingProfileQuery(
        int PageSize,
        int pageIndex
        ) : IRequest<Pagination<MissingChildDto>>;

    public class GetMissingProfileQueryHandler : IRequestHandler<GetMissingProfileQuery, Pagination<MissingChildDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetMissingProfileQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetMissingProfileQueryHandler(IUnitOfWork unitOfWork 
            , ILogger<GetMissingProfileQueryHandler> logger
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        

        async Task<Pagination<MissingChildDto>> IRequestHandler<GetMissingProfileQuery, Pagination<MissingChildDto>>.Handle(GetMissingProfileQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<MissingChild, Guid>();
            var spec = new ChildProfileWithPhotosSpecification(request.PageSize, request.pageIndex);
            var count = await repo.GetCount();

            var missingProfiles = await repo.GetAllWithSpecAsync(spec);
            if (!missingProfiles.Any())
                _logger.LogWarning("Not found any tuple in table , not found data.");

            var profilesDto = _mapper.Map<IReadOnlyList<MissingChildDto>>(missingProfiles);

            return new Pagination<MissingChildDto>
            {
                 PageIndex = request.pageIndex,
                 PageSize = request.PageSize,
                 Count = count,
                 Data = profilesDto
            };
        }
    }
}
