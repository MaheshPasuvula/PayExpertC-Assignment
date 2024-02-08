using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseStudyAssignment.Model;
using CaseStudyAssignment.Repository;

namespace CaseStudyAssignment.Service
{
    internal interface IEmployeeService
    {
         void GetEmployeeById();
        void GetAllEmployees();
         void AddEmployee();
         void UpdateEmployee();
         void RemoveEmployee();
    }
}
