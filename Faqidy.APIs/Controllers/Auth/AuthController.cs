using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Abstraction.Services.Auth;
using Faqidy.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.APIs.Controllers.Auth
{
    public class AuthController(IAuthService _authService) : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<Result<UserDto>>> login(LoginDto model)
        {
            return Ok(await _authService.LoginAsync(model));
        }

        [HttpPost("register")]
        public async Task<ActionResult<Result<UserDto>>> Register(RegisterDto model)
        {
            return Ok(await _authService.RegisterAsync(model));
        }

        [HttpPost("send-otp")]
        public async Task<ActionResult<Result<MessageResource>>> Sendotp(string phoneNumber , string code)
        {
            return Ok(await _authService.SendOtp(phoneNumber , code));
        }

        [HttpPost("generate-otp")]
        public async Task<ActionResult<Result<string>>> GenerateOtp(string user_id)
        {
            return Ok(await _authService.GenerateAndStoreOtp(user_id, TimeSpan.FromMinutes(10)));
        }

        [HttpPost("confirmed-otp")]
        public async Task<ActionResult<Result<UserDto>>> ConfirmOtp(string user_id , string code)
        {
            return Ok(await _authService.ValidateOtp(user_id, code));
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<Result<string>>> UpdateProfile([FromForm]ApplicationUserDto model)
        {
            var result = await _authService.UpdateProfileAsync(User, model);
            return Ok(result);
        }

        [HttpGet("{Id}/profile")]
        public async Task<ActionResult<Result<ApplicationUserDto>>> GetUserProfile([FromRoute]Guid Id)
        {
            return Ok(await _authService.GetUserByIdAsync(Id));
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Result<string>>> DeleteProfile()
        {
            var result = await _authService.DeleteProfileAsync(User);
            return Ok(result);
        }
    }
}
