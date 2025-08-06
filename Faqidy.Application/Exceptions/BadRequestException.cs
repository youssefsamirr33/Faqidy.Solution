using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
         : base("Bad Request, you have made.") { }

        public BadRequestException(string message)
            : base(message) { }

        public BadRequestException(string message , Exception inner)
            : base(message , inner) { }

       

    }
}
