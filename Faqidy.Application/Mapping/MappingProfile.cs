using AutoMapper;
using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Abstraction.Services;
using Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile;
using Faqidy.Application.SocialMedia.MissingProfile.DTOs;
using Faqidy.Application.SocialMedia.Posts.Dtos;
using Faqidy.Domain.Entities.IdentityModule;
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
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl));

            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

            CreateMap<PostDto, SocialPost>()
                .ForMember(dest => dest.ChildId , otp => otp.MapFrom(src => src.ChildProfileId));

            CreateMap<SocialPost, PostReponseDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName +" "+ src.User.LastName))
                .ForMember(dest => dest.ChildProfileId, otp => otp.MapFrom(src => src.ChildId));
               
        }
    }
}
