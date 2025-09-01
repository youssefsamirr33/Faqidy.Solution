using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Common;
using Faqidy.Domain.Entities.IdentityModule;
using System.Security.Claims;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto model);
        Task<Result<UserDto>> RegisterAsync(RegisterDto model);

        Task<ApplicationUser?> GetUserByEmailOrPhoneNumberAsync(string  emailOrPhoneNumber);
        Task<Result<string>> GenerateAndStoreOtp(string user_id, TimeSpan timeForExp);
        Task<Result<UserDto>> ValidateOtp(string user_id, string code, int MaxAttempts = 5);
        Task<Result<MessageResource>> SendOtp(string phoneNumber, string code);
        Task<Result<string>> UpdateProfileAsync(ClaimsPrincipal User , ApplicationUserDto model);
        Task<Result<string>> DeleteProfileAsync(ClaimsPrincipal User);
        Task<Result<ApplicationUserDto>> GetUserByIdAsync(Guid Id);
    }
}
