using Faqidy.Application.Abstraction.Services.OTP;
using Faqidy.Application.Exceptions;
using Faqidy.Domain.Contract.Redis_Repo;
using Faqidy.Domain.Contract.SMS;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.Otp;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.Application.Services.OTP
{
    public class OtpServices(UserManager<ApplicationUser> _userManager ) : IOtpServices
    {

      

    }
}
