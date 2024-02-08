using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Service
{
    internal interface ITaxService
    {
        void GetAllTaxes();
        void CalculateTax();
        void GetTaxById();
        void GetTaxesForEmployee();
         void GetTaxesForYear();
    }
}
