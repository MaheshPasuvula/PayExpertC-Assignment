using CaseStudyAssignment.Service;
using CaseStudyAssignment.Model;
using CaseStudyAssignment.Exceptions;
using CaseStudyAssignment.Repository;

namespace CaseStudyAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEmployeeService employeeService = new EmployeeService();
            IPayRollService payRollService = new PayRollService();
            ITaxService taxService = new TaxService();
            IFinancialRecordService financialRecordService = new FinancialRecordService();
            bool userContinue = true;
            while (userContinue)
            {
            Console.WriteLine("Welcome To The PayRoll Expert");
            Console.WriteLine("1.Emoloyees Service Management");
            Console.WriteLine("2.PayRoll Service Management");
            Console.WriteLine("3.Tax Service Management");
            Console.WriteLine("4.Financial Record Service Management");
            Console.WriteLine("5.EXIT");
            Console.WriteLine("Enter option");
                int userChoicePayExpert = int.Parse(Console.ReadLine());
                switch (userChoicePayExpert)
                {
                    case 1:
                        bool usercontinueEmployee = true;
                        while (usercontinueEmployee)
                        {
                            Console.WriteLine("Welcome to Employee Service Management System");
                            Console.WriteLine("1.GetEmployeeByID");
                            Console.WriteLine("2.GetAllEmoloyees");
                            Console.WriteLine("3.AddEmployee");
                            Console.WriteLine("4.UpdateEmployee");
                            Console.WriteLine("5.RemoveEmployee");
                            Console.WriteLine("6.EXIT");
                            Console.WriteLine("Enter option");
                            int userChoice = int.Parse(Console.ReadLine());
                            switch (userChoice)
                            {
                                case 1:

                                    employeeService.GetEmployeeById();
                                    break;
                                case 2:
                                    employeeService.GetAllEmployees();
                                    break;
                                case 3:
                                    employeeService.AddEmployee();
                                    break;
                                case 4:
                                    employeeService.UpdateEmployee();
                                    break;
                                case 5:
                                    employeeService.RemoveEmployee();
                                    break;
                                case 6:
                                    usercontinueEmployee = false;
                                    break;
                                default:
                                    Console.WriteLine("You selected Wrong Choice Please Check it");
                                    break;
                            }
                        }
                        break;
                    case 2:


                        bool usercontinuePayRoll = true;
                        while (usercontinuePayRoll)
                        {
                            Console.WriteLine("Welcome to PayRoll  Service Management System");
                            Console.WriteLine("1.Generate AllPayROlls");
                            Console.WriteLine("2.Generate PayROll(employeeId, startDate, endDate)");
                            Console.WriteLine("3.Get PayrollById(payrollId)");
                            Console.WriteLine("4.Get PayrollsForEmployee(employeeId)");
                            Console.WriteLine("5.Get PayrollsForPeriod(startDate, endDate)");
                            Console.WriteLine("6.EXIT");
                            Console.WriteLine("Enter option");
                            int userChoicePayroll = int.Parse(Console.ReadLine());
                            switch (userChoicePayroll)
                            {
                                case 1:

                                    payRollService.GenerateAllPayRolls();
                                    break;
                                case 2:
                                    payRollService.GeneratePayRoll();
                                    break;
                                case 3:
                                    payRollService.GetPayROllById();
                                    break;
                                case 4:
                                    payRollService.GetPayrollsForEmployee();
                                    break;
                                case 5:
                                    payRollService.GetPayrollsForPeriod();
                                    break;
                                case 6:
                                    usercontinuePayRoll = false;
                                    break;


                                default:
                                    Console.WriteLine("You selected Wrong Choice Please Check it");
                                    break;
                            }
                        }
                        break;

                    case 3:
                        bool usercontinueTaxService = true;
                        while (usercontinueTaxService)
                        {
                            Console.WriteLine("Welcome to Tax  Service Management System");
                            Console.WriteLine("1.All TaxReports");
                            Console.WriteLine("2.Calculate Tax(employeeId,taxYear");
                            Console.WriteLine("3.GetTaxById(taxId)");
                            Console.WriteLine("4.GetTaxesForEmployee(employeeId)");
                            Console.WriteLine("5.GetTaxesForYear(taxYear)");
                            Console.WriteLine("6.EXIT");
                            Console.WriteLine("Enter option");
                            int userChoicePayroll = int.Parse(Console.ReadLine());
                            switch (userChoicePayroll)
                            {
                                case 1:
                                    taxService.GetAllTaxes();
                                    break;
                                case 2:

                                    taxService.CalculateTax();
                                    break;
                                case 3:
                                    taxService.GetTaxById();
                                    break;
                                case 4:
                                    taxService.GetTaxesForEmployee();
                                    break;
                                case 5:
                                    taxService.GetTaxesForYear();
                                    break;

                                case 6:
                                    usercontinueTaxService = false;
                                    break;
                                default:
                                    Console.WriteLine("You selected Wrong Choice Please Check it");
                                    break;
                            }
                        }
                        break;
                    case 4:

                        bool usercontinueFinancialService = true;
                        while (usercontinueFinancialService)
                        {
                            Console.WriteLine("Welcome to Financial Record Service Management System");
                            Console.WriteLine("1.GetAllFinancialRecords");
                            Console.WriteLine("2.AddFinancialRecord");
                            Console.WriteLine("3.GetFinancialRecordById(recordId)");
                            Console.WriteLine("4.GetFinancialRecordsForEmployee(employeeId)");
                            Console.WriteLine("5.GetFinancialRecordsForDate(recordDate)");
                            Console.WriteLine("6.EXIT");
                            Console.WriteLine("Enter option");
                            int userChoicePayroll = int.Parse(Console.ReadLine());
                            switch (userChoicePayroll)
                            {
                                case 1:
                                    financialRecordService.GetAllFinancialRecords();
                                    break;
                                case 2:

                                    financialRecordService.AddFinancialRecord();
                                    break;
                                case 3:
                                    financialRecordService.GetFinancialRecordById();
                                    break;
                                case 4:
                                    financialRecordService.GetFinancialRecordsForEmployee();
                                    break;
                                case 5:
                                    financialRecordService.GetFinancialRecordsForDate();
                                    break;

                                case 6:
                                    usercontinueFinancialService = false;
                                    break;
                                default:
                                    Console.WriteLine("You selected Wrong Choice Please Check it");
                                    break;
                            }
                        }
                        break;
                    case 5:
                        userContinue = false;
                        break;
                    default:
                        Console.WriteLine("Entered wrong Choice Please Check it");
                        break;



                }
            }

        }
    }
}