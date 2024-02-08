using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Repository
{
    internal interface ITaxRepository
    {
        string GetAllTaxes();
        string CalculateTax(int userEmployeeId, int userYear);
        string GetTaxById(int userTaxId);
        string GetTaxesForEmployee(int userEmployeeId);
        string GetTaxesForYear(int userYear);
    }
}
