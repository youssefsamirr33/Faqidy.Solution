using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Exceptions
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException() : base("Invalid Login")
        { }

        public UnAuthorizeException(string message)
            :base(message) { }
        
    }
}
