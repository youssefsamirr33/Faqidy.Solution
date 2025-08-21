using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Domain.Entities.IdentityModule;

namespace Faqidy.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);

        Task<ApplicationUser?> GetUserByEmailOrPhoneNumberAsync(string  emailOrPhoneNumber);
    }
}
