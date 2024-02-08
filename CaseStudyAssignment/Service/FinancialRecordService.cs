using CaseStudyAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Service
{
    internal class FinancialRecordService : IFinancialRecordService
    {
        IFinancialRecordRepository financialRecordRepository = new FinancialRecordRepository();
        public void GetAllFinancialRecords()
        {
            string allRecords = financialRecordRepository.GetAllFinancialRecords();
            Console.WriteLine(allRecords);
        }
        public void AddFinancialRecord()
        {
            Console.WriteLine("Enter your EmployeeID");
            int userEmployeeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your Description");
            string userDescription = Console.ReadLine();
            decimal userAmount;

            do
            {
                Console.WriteLine("Enter your Amount (decimal):");
                string userInput = Console.ReadLine();

                if (decimal.TryParse(userInput, out userAmount))
                {
                    // Valid decimal input
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                }

            } while (true);
            string userRecordType;

            do
            {
                Console.WriteLine("Enter Record Type (Income or Expenses):");
                userRecordType = Console.ReadLine().Trim();

                if (userRecordType.Equals("Income", StringComparison.OrdinalIgnoreCase) || userRecordType.Equals("Expenses", StringComparison.OrdinalIgnoreCase))
                {
                    // Valid record type
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid record type. Please choose 'Income' or 'Expenses'.");
                }

            } while (true);

            string conformationMessage = financialRecordRepository.AddFinancialRecord(userEmployeeId, userDescription, userAmount, userRecordType);
            Console.WriteLine(conformationMessage);

        }
        public void GetFinancialRecordById()
        {
            Console.WriteLine("Enter your RecordID");
            int userRecordId = int.Parse(Console.ReadLine());
            string financialRecord = financialRecordRepository.GetFinancialRecordById(userRecordId);
            Console.WriteLine(financialRecord);

        }
        public void GetFinancialRecordsForEmployee()
        {
            Console.WriteLine("Enter your EmployeeID");
            int userEmployeeId = int.Parse(Console.ReadLine());
            string financialRecord = financialRecordRepository.GetFinancialRecordsForEmployee(userEmployeeId);
            Console.WriteLine(financialRecord);

        }
        public void GetFinancialRecordsForDate()
        {
            DateTime userRecordDate;
            do
            {
                Console.WriteLine("Enter your Date (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", null, DateTimeStyles.None, out userRecordDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            string financialRecord = financialRecordRepository.GetFinancialRecordsForDate(userRecordDate);
            Console.WriteLine(financialRecord);


        }
    }
}
