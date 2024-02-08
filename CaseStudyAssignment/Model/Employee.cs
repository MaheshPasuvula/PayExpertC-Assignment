using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Model
{
    internal class Employee
    {
        private int employeeID;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string gender;
        private string email;
        private long phoneNumber;
        private string address;
        private string position;
        private DateTime joiningDate;
        private DateTime terminationDate;
        //default constructor
        public Employee()
        {

        }
        //Parameterized constructor
        public Employee(int employeeID, string firstName, string lastName, DateTime dateOfBirth,
                    string gender, string email, long phoneNumber, string address,
                    string position, DateTime joiningDate, DateTime terminationDate)
        {
            this.employeeID = employeeID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.position = position;
            this.joiningDate = joiningDate;
            this.terminationDate = terminationDate;
        }
        //getter and setter methods
        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public long PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        public DateTime JoiningDate
        {
            get { return joiningDate; }
            set { joiningDate = value; }
        }
        public DateTime TerminationDate
        {
            get { return terminationDate; }
            set { terminationDate = value; }
        }
    }
}
