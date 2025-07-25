using AutoMapper;
using Faqidy.Application.SocialMedia.MissingProfile.Commands.AddMissingProfile;
using Faqidy.Application.SocialMedia.MissingProfile.DTOs;
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
            CreateMap<AddMissingProfileCommands , MissingChild>();
            CreateMap<MissingChild, MissingChildDto>();
        }
    }
}
