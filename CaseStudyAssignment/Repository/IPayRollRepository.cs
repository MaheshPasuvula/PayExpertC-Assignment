using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Repository
{
    internal interface IPayRollRepository
    {
        string GenerateAllPayRolls();
        string GeneratePayRoll(int userEmployeeID, DateTime userStartDate, DateTime userEndDate);
        string GetPayRollById(int userPayRollId);
        string GetPayRollsByEmployee(int userEmployeeId);
        string GetPayrollsForPeriod(DateTime userStartDate, DateTime userEndDate);
        decimal CalculateGrossSalary(int userEmployeeID, DateTime userStartDate, DateTime userEndDate);
        decimal CalculateNetSalary(int userEmployeeID, DateTime userStartDate, DateTime userEndDate);

    }
}
