using CaseStudyAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Service
{
    internal class TaxService : ITaxService
    {
        ITaxRepository taxRepository = new TaxRepository();
        public void GetAllTaxes()
        {
            string getAllTaxes = taxRepository.GetAllTaxes();
            Console.WriteLine(getAllTaxes);
        }
        public void CalculateTax()
        {
            Console.WriteLine("Enter your EmployeeID");
            int userEmployeeId = int.Parse(Console.ReadLine());
            int userYear;

            do
            {
                Console.WriteLine("Enter Year (4 digits):");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out userYear) && userInput.Length == 4)
                {
                    // Valid 4-digit year
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid 4-digit year.");
                }

            } while (true);
            string calcualteTax = taxRepository.CalculateTax(userEmployeeId, userYear);
            Console.WriteLine(calcualteTax);



        }
        public void GetTaxById()
        {
            Console.WriteLine("Enter  your TaxId");
            int userTaxId = int.Parse(Console.ReadLine());
            string taxByid = taxRepository.GetTaxById(userTaxId);
            Console.WriteLine(taxByid);
        }
        public void GetTaxesForEmployee()
        {
            Console.WriteLine("Enter your EmployeeID");
            int userEmployeeId = int.Parse(Console.ReadLine());
            string employeeTax = taxRepository.GetTaxesForEmployee(userEmployeeId);
            Console.WriteLine(employeeTax);

        }
        public void GetTaxesForYear()
        {
            int userYear;

            do
            {
                Console.WriteLine("Enter Year (4 digits):");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out userYear) && userInput.Length == 4)
                {
                    // Valid 4-digit year
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid 4-digit year.");
                }

            } while (true);
            string taxReportYear = taxRepository.GetTaxesForYear(userYear);
            Console.WriteLine(taxReportYear);

        }
    }
}
