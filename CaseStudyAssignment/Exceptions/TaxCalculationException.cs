using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Exceptions
{
    internal class TaxCalculationException:ApplicationException
    {
    public string Message;
        public TaxCalculationException()
        {
            Message = "TaxCalcualtionException For Your Entered Details There Is No Records";
        }
        public TaxCalculationException(string message) : base(message)
        {

        }
    }
}
