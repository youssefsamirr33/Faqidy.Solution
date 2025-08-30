using Faqidy.Domain.Contract.SMS;
using Faqidy.Infrastructure.SMS_Provider.Dto;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Faqidy.Infrastructure.SMS_Provider
{
    public class SMSServices : ISMSServices
    {
        private readonly TwilioSettings _twilio;

        public SMSServices(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }
        public async Task<MessageResource> SendAsync(string phoneNumber, string body)
        {
            TwilioClient.Init("AC55896acce8a056942463635f5d207228", "76407dec148cc64cec5c7608428ee379");

            var message = await MessageResource.CreateAsync(
                from : new Twilio.Types.PhoneNumber("+14632858621"),
                to : new Twilio.Types.PhoneNumber(phoneNumber),
                body : body
                );
            return message;
        }
    }
}
