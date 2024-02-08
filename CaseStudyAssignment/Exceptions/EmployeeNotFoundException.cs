using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Exceptions
{
    public class EmployeeNotFoundException:ApplicationException
    {
        public string Message;
        public EmployeeNotFoundException()
        {
            Message = "Employee NotFound Exception";
        }
        public EmployeeNotFoundException(string message) : base(message)
        {

        }
    }
}
