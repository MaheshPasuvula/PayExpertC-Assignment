using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Repository
{
    internal interface IEmployeeRepository
    {
        string GetEmployeeById(int userEmployeeID);
        string GetAllEmployees();
        string AddEmployee(string userFirstName, string userLastName, DateTime userDateOfBirth, string userGender, string userEmail, string userPhoneNumber, string userAddress, string userPosition, DateTime userJoiningDate, DateTime userTerminationDate);
        string UpdateFirstName(string userFirstName, int userEmployeeID);
        string UpdateLastName(string userLastName, int userEmployeeID);
        string UpdateDateOfBirth(DateTime userDateOfBirth, int userEmployeeID);
        string UpdateGender(string userGender, int userEmployeeID);
        string UpdateEmail(string userEmail, int userEmployeeID);
        string UpdatePhoneNumber(string userEmail, int userEmployeeID);
        string UpdateAddress(string userAddress, int userEmployeeID);
        string UpdatePosition(string userPosition, int userEmployeeID);
        string UpdateJoiningDate(DateTime userJoiningDate, int userEmployeeID);
        public DateTime GetJoiningDate(int userEmployeeID);
        string UpdateTerminationDate(DateTime userTerminationDate, int userEmployeeID);
        public string RemoveEmployee(int userEmployeeID);



    }
}
