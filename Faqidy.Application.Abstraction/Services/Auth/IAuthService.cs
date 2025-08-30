using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Domain.Entities.IdentityModule;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);

        Task<ApplicationUser?> GetUserByEmailOrPhoneNumberAsync(string  emailOrPhoneNumber);
        Task<string> GenerateAndStoreOtp(string user_id, TimeSpan timeForExp);
        Task<(bool status, string message)> ValidateOtp(string user_id, string code, int MaxAttempts = 5);
        Task<MessageResource> SendOtp(string phoneNumber, string code);
    }
}
