using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Abstraction.Services.OTP
{
    public interface IOtpServices
    {
        Task<string> GenerateAndStoreOtp(string user_id, TimeSpan timeForExp);
        Task<(bool status, string message)> ValidateOtp(string user_id, string code, int MaxAttempts = 5);
    }
}
