using Faqidy.Application.Abstraction.DTOs.Otp;
using Faqidy.Application.Abstraction.Services.OTP;
using Faqidy.Application.Exceptions;
using Faqidy.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Faqidy.Application.Services.OTP
{
    public class OtpServices(IDistributedCache _cache , UserManager<ApplicationUser> _userManager) : IOtpServices
    {

        public async Task<string> GenerateAndStoreOtp(string user_id, TimeSpan timeForExp)
        {
            var code = GenerateRandomNumber();
            var otp = new OtpPayload()
            {
                code = code,
                exp = DateTime.UtcNow,
            };

            var json = JsonSerializer.Serialize(otp);
            await _cache.SetStringAsync(user_id, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeForExp
            });

            return code;
        }

        public async Task<(bool status, string message)> ValidateOtp(string user_id, string code, int MaxAttempts = 5)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user is null) throw new NotFoundException($"The user with id: {user_id} is not found. ");

            var json = await _cache.GetStringAsync(user_id);
            if (json is null) throw new BadRequestException("not valid otp");
            var otp = JsonSerializer.Deserialize<OtpPayload>(json!);

            if(DateTime.UtcNow > otp!.exp)
            {
                await _cache.RemoveAsync(user_id);
                throw new BadRequestException("The opt is expired");
            }

            otp.Attempts++;
            if(otp.Attempts > MaxAttempts)
            {
                await _cache.RemoveAsync(user_id);
                throw new BadRequestException("Too many attemps");
            }
            if(!string.Equals(code , otp.code, StringComparison.OrdinalIgnoreCase))
            {
                var update = JsonSerializer.Serialize(otp);
                await _cache.SetStringAsync(user_id, update, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.Parse(otp.exp.ToString())
                });
                throw new BadRequestException("Invalid otp");
            }

            await _cache.RemoveAsync(user_id);
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
