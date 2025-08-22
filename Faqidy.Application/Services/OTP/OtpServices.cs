using Faqidy.Application.Abstraction.Services.OTP;
using Faqidy.Application.Exceptions;
using Faqidy.Domain.Contract.Redis_Repo;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.Otp;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Faqidy.Application.Services.OTP
{
    public class OtpServices(UserManager<ApplicationUser> _userManager , IRedisRepository _redis) : IOtpServices
    {

        public async Task<string> GenerateAndStoreOtp(string user_id, TimeSpan timeToLive)
        {
            var code = GenerateRandomNumber();
            var otp = new OtpPayload()
            {
                code = code,
                exp = DateTime.UtcNow,
            };
            await _redis.AddOrUpdateAsyn(user_id , otp , timeToLive);
            
            return code;
        }

        public async Task<(bool status, string message)> ValidateOtp(string user_id, string code, int MaxAttempts = 5)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user is null) throw new NotFoundException($"The user with id: {user_id} is not found. ");

            var otp = await _redis.GetAsync(user_id);
            if (otp is null) throw new BadRequestException("not valid otp");

            if(DateTime.UtcNow > otp!.exp)
            {
                await _redis.RemoveAsync(user_id);
                throw new BadRequestException("The opt is expired");
            }

            otp.Attempts++;
            if(otp.Attempts > MaxAttempts)
            {
                await _redis.RemoveAsync(user_id);
                throw new BadRequestException("Too many attemps");
            }
            if(!string.Equals(code , otp.code, StringComparison.OrdinalIgnoreCase))
            {
                await _redis.AddOrUpdateAsyn(user_id, otp, TimeSpan.Parse(otp.exp.ToString()));
                throw new BadRequestException("Invalid otp");
            }

            await _redis.RemoveAsync(user_id);
            user.PhoneNumberConfirmed = true;
            user.EmailConfirmed = true;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new BadRequestException("The user information is not updated successfuly");
            return (true, "The Otp is verified");
        }

        private string GenerateRandomNumber()
        {
            var random = RandomNumberGenerator.GetInt32(0, 1000000);
            return random.ToString("D6");
        }
    }
}
