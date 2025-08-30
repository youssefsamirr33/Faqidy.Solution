using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Abstraction.Services.Auth;
using Faqidy.Application.Abstraction.Services.OTP;
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

        [HttpPost("send-otp")]
        public async Task<ActionResult> Sendotp(string phoneNumber , string code)
        {
            return Ok(await _authService.SendOtp(phoneNumber , code));
        }

        [HttpPost("generate-otp")]
        public async Task<ActionResult> GenerateOtp(string user_id)
        {
            return Ok(await _authService.GenerateAndStoreOtp(user_id, TimeSpan.FromMinutes(10)));
        }

        [HttpPost("confirmed-otp")]
        public async Task<ActionResult> ConfirmOtp(string user_id , string code)
        {
            return Ok(await _authService.ValidateOtp(user_id, code));
        }


    }
}
