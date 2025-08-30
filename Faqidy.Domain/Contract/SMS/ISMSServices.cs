using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.Domain.Contract.SMS
{
    public interface ISMSServices
    {
        Task<MessageResource> SendAsync(string phoneNumber, string body);
    }
}
