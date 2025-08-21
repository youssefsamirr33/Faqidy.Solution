using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Abstraction.DTOs.Auth
{
    public class JwtSettings
    {
        public required string key { get; set; }
        public required string audience { get; set; }
        public required string issuer { get; set; }
        public required double expInMinutes { get; set; }
    }
}
