using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Abstraction.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromForm]ApplicationUserDto model)
        {
            var result = await _authService.UpdateProfileAsync(User, model);
            return Ok(new
            {
                status = result.status,
                message = result.message
            });
        }

        [HttpGet("{Id}/profile")]
        public async Task<IActionResult> GetUserProfile([FromRoute]Guid Id)
        {
            return Ok(await _authService.GetUserByIdAsync(Id));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProfile()
        {
            var result = await _authService.DeleteProfileAsync(User);
            return Ok(new
            {
                status = result.status,
                message = result.message
            });
        }
    }
}
