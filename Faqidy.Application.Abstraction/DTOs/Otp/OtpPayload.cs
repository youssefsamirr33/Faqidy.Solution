using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Abstraction.DTOs.Otp
{
    public class OtpPayload
    {
        public string code { get; set; }
        public DateTime exp { get; set; }
        public int Attempts { get; set; }
    }
}
