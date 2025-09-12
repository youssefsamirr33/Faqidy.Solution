using AutoMapper;
using Faqidy.Application.Common;
using Faqidy.Application.Exceptions;
using Faqidy.Application.SocialMedia.MissingProfile.DTOs;
using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.SotialMediaModule;
using Faqidy.Domain.Specification.Child_profile;
using MediatR;

namespace Faqidy.Application.SocialMedia.MissingProfile.Queries
{
    public record GetMissingProfileByIdQuery(Guid Id) : IRequest<Result<MissingChildDto>>;
    public class GetMissingProfileByIdHandler : IRequestHandler<GetMissingProfileByIdQuery, Result<MissingChildDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMissingProfileByIdHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        async Task<Result<MissingChildDto>> IRequestHandler<GetMissingProfileByIdQuery, Result<MissingChildDto>>.Handle(GetMissingProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var _repo = _unitOfWork.GetRepository<MissingChild, Guid>();

            var spec = new ChildProfileWithPhotosSpecification(request.Id);
            var profile = await _repo.GetWithSpecAsync(spec);

            if(profile is null) 
                throw new NotFoundException(typeof(MissingChild).Name , request.Id);

            var profileDto = _mapper.Map<MissingChildDto>(profile);
            return Result<MissingChildDto>.Success(profileDto);
        }
    }
}
