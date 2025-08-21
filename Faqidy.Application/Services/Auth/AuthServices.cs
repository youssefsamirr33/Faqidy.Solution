using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Application.Abstraction.Services.Auth;
using Faqidy.Application.Exceptions;
using Faqidy.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Services.Auth
{
    public class AuthServices(
        UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager ,
        IOptions<JwtSettings> jwtSettings
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
                Token = await GenerateTokenAsync(user),
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
