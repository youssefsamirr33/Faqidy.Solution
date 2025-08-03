using AutoMapper;
using Faqidy.Application.Abstraction.Services;
using Faqidy.Application.SocialMedia.MissingProfile.DTOs;
using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Entities.SotialMediaModule;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile
{
    public class AddMissingProfileHandler : IRequestHandler<AddMissingProfileCommands, MissingChildDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddMissingProfileHandler> _logger;
        private readonly IPhotosServices _photosServices;

        public AddMissingProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddMissingProfileHandler> logger
            , IPhotosServices photosServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _photosServices = photosServices;
        }

        async Task<MissingChildDto> IRequestHandler<AddMissingProfileCommands, MissingChildDto>.Handle(AddMissingProfileCommands request, CancellationToken cancellationToken)
        {
            // 1- get missing child info 
            var missingChild = _mapper.Map<MissingChild>(request);

            // save photo
            var photoPaths = new List<string>(); 
            if (request.Photos.Any() || request.Photos != null)
            {
                try
                {
                    // save photo with called photo service  
                    photoPaths = await _photosServices.SavePhotosAsync(request.Photos);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Error when occured saved the photos.");
                }
            }
            // 3. Add photo entities to missing child
            missingChild.Photos = photoPaths.Select(path => new ChildPhoto
            {
                PhotoUrl = path,
                UploadDate = DateTime.UtcNow
            }).ToList();


            // save missing child info 
            try
            {
                // demo 
                missingChild.CreatedBy = "youssef";
                missingChild.LastModifiedBy = "youssef";
                // demo 
                var repo = _unitOfWork.GetRepository<MissingChild, Guid>();
                await repo.AddAsync(missingChild);
                var result = await _unitOfWork.CompleteAsync();

                if (result <= 0)
                    _logger.LogError("Error When Save Changes for add new missing child");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error When Add new missing child.");
            }

            // return missing child Dto
            var ReturnedMissingchild = _mapper.Map<MissingChildDto>(missingChild);

            if(ReturnedMissingchild.ChildPhotos is not null)
            {
                foreach(var photoDto in ReturnedMissingchild.ChildPhotos)
                {
                    var orginalPhoto = missingChild.Photos.FirstOrDefault(p => p.Id == photoDto.Id);
                    if(orginalPhoto is not null)
                    {
                        photoDto.PhotoUrl = _photosServices.GetPhotoUrl(orginalPhoto.PhotoUrl);
                    }
                }
            }

            return ReturnedMissingchild;

        }
    }
}
