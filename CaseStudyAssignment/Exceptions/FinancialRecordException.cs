using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Exceptions
{
    internal class FinancialRecordException:ApplicationException
    {
        public string Message;
        public FinancialRecordException()
        {
            Message = "FinancialRecord Exception";
        }
        public FinancialRecordException(string message) : base(message)
        {

        }
    }
}
