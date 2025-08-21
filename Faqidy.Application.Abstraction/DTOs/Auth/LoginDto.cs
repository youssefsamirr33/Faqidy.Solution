using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Abstraction.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public required string EmailOrPhoneNumber { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
