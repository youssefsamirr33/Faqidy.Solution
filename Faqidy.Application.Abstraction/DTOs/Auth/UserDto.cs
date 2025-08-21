﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Abstraction.DTOs.Auth
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string userName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }

    }
}
