using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Abstraction.Services.Auth;
using Faqidy.Application.Exceptions;
using Faqidy.Domain.Contract.Redis_Repo;
using Faqidy.Domain.Contract.SMS;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.Otp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.Application.Services.Auth
{
    public class AuthServices(
        UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager ,
        IOptions<JwtSettings> jwtSettings,
        IRedisRepository _redis,
        ISMSServices _sms
        ) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                gender = model.gender
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) throw new BadRequestException("Error when registered");

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                userName = user.UserName,
                Token = "send otp"
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user =await GetUserByEmailOrPhoneNumberAsync(model.EmailOrPhoneNumber);
            if(user is null) throw new UnAuthorizeException("Invalid Login");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if(result.IsLockedOut) throw new Exception("The email is locked out , please try again after 30 sec");
            else if (result.IsNotAllowed) throw new Exception("The Email or Phone Number is not verified.");
            else if (!result.Succeeded) throw new UnAuthorizeException("Invalid login");

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                userName = user.UserName!,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<string> GenerateAndStoreOtp(string user_id, TimeSpan timeToLive)
        {
            var code = GenerateRandomNumber();
            var otp = new OtpPayload()
            {
                code = code,
                exp = DateTime.UtcNow.AddMinutes(30),
            };
            await _redis.AddOrUpdateAsyn(user_id, otp, timeToLive);

            return code;
        }

        public async Task<MessageResource> SendOtp(string phoneNumber, string code)
        {
            if (string.IsNullOrEmpty(code)) throw new BadRequestException("The Code is not found.");
            var body = $"This is the otp {code} to verivied your account.";
            var result = await _sms.SendAsync(phoneNumber, body);

            return result;
        }

        public async Task<(bool status, string message)> ValidateOtp(string user_id, string code, int MaxAttempts = 5)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user is null) throw new NotFoundException($"The user with id: {user_id} is not found. ");

            var otp = await _redis.GetAsync(user_id);
            if (otp is null) throw new BadRequestException("not valid otp");

            if (DateTime.UtcNow > otp!.exp)
            {
                await _redis.RemoveAsync(user_id);
                throw new BadRequestException("The opt is expired");
            }

            otp.Attempts++;
            if (otp.Attempts > MaxAttempts)
            {
                await _redis.RemoveAsync(user_id);
                throw new BadRequestException("Too many attemps");
            }
            if (!string.Equals(code, otp.code, StringComparison.OrdinalIgnoreCase))
            {
                await _redis.AddOrUpdateAsyn(user_id, otp, TimeSpan.Parse(otp.exp.ToString()));
                throw new BadRequestException("Invalid otp");
            }

            await _redis.RemoveAsync(user_id);
            user.PhoneNumberConfirmed = true;
            user.EmailConfirmed = true;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new BadRequestException("The user information is not updated successfuly");
            return (true, $"The Otp is verified , this is the token {await GenerateTokenAsync(user)}");
        }

        private string GenerateRandomNumber()
        {
            var random = RandomNumberGenerator.GetInt32(0, 1000000);
            return random.ToString("D6");
        }


        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Upn, user.UserName!),
                new Claim("Id" , user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key));
            var credintial = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            // create token object 
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.issuer,
                audience: _jwtSettings.audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.expInMinutes),
                claims: claims,
                signingCredentials: credintial
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public async Task<ApplicationUser?> GetUserByEmailOrPhoneNumberAsync(string emailOrPhoneNumber)
            => emailOrPhoneNumber.Contains("@") ? await _userManager.FindByEmailAsync(emailOrPhoneNumber) :
                 await _userManager.Users.Where(u => u.PhoneNumber == emailOrPhoneNumber).FirstOrDefaultAsync();

    }
}
