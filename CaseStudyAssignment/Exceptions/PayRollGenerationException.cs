using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Exceptions
{
    internal class PayRollGenerationException : ApplicationException
    {
        public string Message;
        public PayRollGenerationException()
        {
            Message = "PayRoll Generation Exception";
        }
        public PayRollGenerationException(string message) : base(message)
        {

        }
    }
}
