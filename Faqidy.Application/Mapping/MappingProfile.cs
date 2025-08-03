using AutoMapper;
using Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile;
using Faqidy.Application.SocialMedia.MissingProfile.DTOs;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Entities.SotialMediaModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddMissingProfileCommands, MissingChild>()
                .ForMember(dest => dest.Photos , opt => opt.Ignore());

            // Entity to DTO mapping for response
            CreateMap<MissingChild, MissingChildDto>()
                .ForMember(dest => dest.ChildPhotos, opt => opt.MapFrom(src => src.Photos)); // Explicitly map photos

            CreateMap<ChildPhoto, ChildPhotoDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());
        }
    }
}
