using Faqidy.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Abstraction.DTOs.Auth
{
    public class ApplicationUserDto
    {
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public Gender? gender { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? NationalId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
