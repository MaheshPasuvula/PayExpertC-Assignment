using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Model
{
    public class Payroll
    {
        private int payrollID;
        private int employeeID;
        private DateTime payPeriodStartDate;
        private DateTime payPeriodEndDate;
        private decimal basicSalary;
        private decimal overTimePay;
        private decimal deductions;
        private decimal netSalary;
        public Payroll()
        {

        }
        public Payroll(int payrollID, int employeeID, DateTime payPeriodStartDate, DateTime payPeriodEndDate,
            decimal basicSalary, decimal overTimePay, decimal deductions, decimal netSalary)
        {
            this.payrollID = payrollID;
            this.employeeID = employeeID;
            this.payPeriodStartDate = payPeriodStartDate;
            this.payPeriodEndDate = payPeriodEndDate;
            this.basicSalary = basicSalary;
            this.overTimePay = overTimePay;
            this.deductions = deductions;
            this.netSalary = netSalary;
        }
        public int PayrollID
        {
            get { return payrollID; }
            set { payrollID = value; }
        }
        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        public DateTime PayPeriodStartDate
        {
            get { return payPeriodStartDate; }
            set { payPeriodStartDate = value; }
        }
        public DateTime PayPeriodEndDate
        {
            get { return payPeriodEndDate; }
            set { payPeriodEndDate = value; }
        }
        public decimal BasicSalary
        {
            get { return basicSalary; }
            set { basicSalary = value; }
        }
        public decimal OverTimePay
        {
            get { return overTimePay; }
            set { overTimePay = value; }
        }
        public decimal Deductions
        {
            get { return deductions; }
            set { deductions = value; }
        }
        public decimal NetSalary
        {
            get { return netSalary; }
            set { netSalary = value; }
        }


    }
}
