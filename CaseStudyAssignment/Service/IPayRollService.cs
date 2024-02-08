using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Service
{
    internal interface IPayRollService
    {
        void GenerateAllPayRolls();
       void GeneratePayRoll();
       void GetPayROllById();
        void GetPayrollsForEmployee();
       void GetPayrollsForPeriod();
        void CalculateNetSalary();

    }
}
