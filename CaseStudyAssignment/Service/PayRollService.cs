using CaseStudyAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Service
{
    internal class PayRollService : IPayRollService
    {
        IPayRollRepository payRollRepository = new PayRollRepository();
        public void GenerateAllPayRolls()
        {
            string allPayRolls = payRollRepository.GenerateAllPayRolls();
            Console.WriteLine(allPayRolls);

        }
        public void GeneratePayRoll()
        {
            Console.WriteLine("Enter EmployeeID");
            int userEmployeeID = int.Parse(Console.ReadLine());
            DateTime userStartDate;
            do
            {
                Console.WriteLine("Enter your StartDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userStartDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            DateTime userEndDate;
            do
            {
                Console.WriteLine("Enter your EndDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userEndDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            string payRoll = payRollRepository.GeneratePayRoll(userEmployeeID, userStartDate, userEndDate);
            Console.WriteLine(payRoll);
        }
        public void GetPayROllById()
        {
            Console.WriteLine("Enter your PayRollId");
            int userPayRollId = int.Parse(Console.ReadLine());
            string payrollbyid = payRollRepository.GetPayRollById(userPayRollId);
            Console.WriteLine(payrollbyid);
        }
        public void GetPayrollsForEmployee()
        {
            Console.WriteLine("Enter your EmployeeID");
            int userEmployeeId = int.Parse(Console.ReadLine());
            string payrollforemployee = payRollRepository.GetPayRollsByEmployee(userEmployeeId);
            Console.WriteLine(payrollforemployee);

        }
        public void GetPayrollsForPeriod()
        {
            DateTime userStartDate;
            do
            {
                Console.WriteLine("Enter your StartDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userStartDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            DateTime userEndDate;
            do
            {
                Console.WriteLine("Enter your EndDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userEndDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            string payrollforperiod = payRollRepository.GetPayrollsForPeriod(userStartDate, userEndDate);
            Console.WriteLine(payrollforperiod);

        }
        public void CalculateGrossSalary()
        {
            Console.WriteLine("Enter EmployeeID");
            int userEmployeeID = int.Parse(Console.ReadLine());
            DateTime userStartDate;
            do
            {
                Console.WriteLine("Enter your StartDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userStartDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            DateTime userEndDate;
            do
            {
                Console.WriteLine("Enter your EndDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userEndDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            decimal grossSalary = payRollRepository.CalculateGrossSalary(userEmployeeID, userStartDate, userEndDate);
            Console.WriteLine(grossSalary);
        }
        public void CalculateNetSalary()
        {
            Console.WriteLine("Enter EmployeeID");
            int userEmployeeId = int.Parse(Console.ReadLine());
            DateTime userStartdate;
            do
            {
                Console.WriteLine("Enter your StartDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userStartdate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            DateTime userEnddate;
            do
            {
                Console.WriteLine("Enter your EndDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userEnddate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            decimal netSalary = payRollRepository.CalculateNetSalary(userEmployeeId, userStartdate, userEnddate);
            Console.WriteLine(netSalary);
        }
    }
}
