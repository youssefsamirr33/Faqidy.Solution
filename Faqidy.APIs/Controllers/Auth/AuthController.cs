using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Abstraction.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faqidy.APIs.Controllers.Auth
{
    public class AuthController(IAuthService _authService) : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDto model)
        {
            return Ok(await _authService.LoginAsync(model));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            return Ok(await _authService.RegisterAsync(model));
        }
    }
}
