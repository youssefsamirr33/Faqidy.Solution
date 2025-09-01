using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Domain.Entities.IdentityModule;
using System.Security.Claims;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);

        Task<ApplicationUser?> GetUserByEmailOrPhoneNumberAsync(string  emailOrPhoneNumber);
        Task<string> GenerateAndStoreOtp(string user_id, TimeSpan timeForExp);
        Task<UserDto> ValidateOtp(string user_id, string code, int MaxAttempts = 5);
        Task<MessageResource> SendOtp(string phoneNumber, string code);
        Task<(bool status , string message)> UpdateProfileAsync(ClaimsPrincipal User , ApplicationUserDto model);
        Task<(bool status , string message)> DeleteProfileAsync(ClaimsPrincipal User);
        Task<ApplicationUserDto> GetUserByIdAsync(Guid Id);
    }
}
