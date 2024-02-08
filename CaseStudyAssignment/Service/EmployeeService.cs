using System;
using CaseStudyAssignment.Repository;
using CaseStudyAssignment.Service;
using CaseStudyAssignment.Model;
using CaseStudyAssignment.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace CaseStudyAssignment.Service
{
    internal class EmployeeService : IEmployeeService
    {
        static bool IsValidString(string userinputname)
        {
            return !string.IsNullOrEmpty(userinputname) && userinputname.All(char.IsLetter);
        }
        static bool IsValidGender(string userGender)
        {
            return (userGender == "male" || userGender == "female");
        }
        static bool IsValidEmail(string email)
        {
            // Regular expression for a simple email validation
            string emailPattern = @"^[a-zA-Z0-9._-]+@gmail\.com$";

            // Use Regex.IsMatch to check if the input matches the email pattern
            return Regex.IsMatch(email, emailPattern);
        }
        static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regular expression for a valid phone number format
            string phonePattern = @"^(\+91|91)?[1-9][0-9]{9}$";

            // Use Regex.IsMatch to check if the input matches the phone pattern
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, phonePattern);
        }
        IEmployeeRepository employeeRepository = new EmployeeRepository();
        public void GetEmployeeById()
        {
            try
            {
                Console.WriteLine("Enter your EmployeeID");
                int userEmployeeID = int.Parse(Console.ReadLine());
                string employeedetails = employeeRepository.GetEmployeeById(userEmployeeID);
                Console.WriteLine(employeedetails);
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                if (ex.Number == -2) // Assuming -2 is the error number for timeout
                {
                    Console.WriteLine("SQL Timeout Exception occurred: " + ex.Message);
                    // Implement retry logic, notify the user, log the error, etc.
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }



        }
        public void GetAllEmployees()
        {
            String getAllEmployees = employeeRepository.GetAllEmployees();
            Console.WriteLine(getAllEmployees);
        }
        public void AddEmployee()
        {
            string userFirstName;
            do
            {
                Console.WriteLine("Enter your FirstName: ");
                userFirstName = Console.ReadLine();

                if (!IsValidString(userFirstName))
                {
                    Console.WriteLine("Invalid format. Please enter a valid string.");
                }

            } while (!IsValidString(userFirstName));
            string userLastName;
            do
            {
                Console.WriteLine("Enter your LastName: ");
                userLastName = Console.ReadLine();

                if (!IsValidString(userLastName))
                {
                    Console.WriteLine("Invalid format. Please enter a valid string.");
                }

            } while (!IsValidString(userLastName));

            DateTime userDateOfBirth;
            do
            {
                Console.WriteLine("Enter your DateOfBirth (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userDateOfBirth))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            string userGender;
            do
            {
                Console.WriteLine("Enter your Gender (male/female): ");
                userGender = Console.ReadLine().ToLower(); // Convert input to lowercase for case-insensitive comparison

                if (IsValidGender(userGender))
                {
                    break; // Exit the loop if the input is a valid gender
                }
                else
                {
                    Console.WriteLine("Invalid gender. Please enter a valid gender (male, female).");
                }

            } while (true);
            string userEmail;
            do
            {
                Console.WriteLine("Enter your email: ");
                userEmail = Console.ReadLine();

                if (IsValidEmail(userEmail))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid email format or does not end with @gmail.com. Please enter a valid Gmail address.");
                }
            } while (true);
            string userPhoneNumber;
            do
            {
                Console.WriteLine("Enter your PhoneNumber: ");
                userPhoneNumber = (Console.ReadLine());

                if (IsValidPhoneNumber(userPhoneNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid phone number format. Please enter a valid 10-digit phone number starting with +91 or 91.");
                }
            } while (true);

            Console.WriteLine("Enter your Address");
            string userAddress = Console.ReadLine();
            string userPosition;
            do
            {
                Console.WriteLine("Enter your Position: ");
                userPosition = Console.ReadLine();

                if (!IsValidString(userPosition))
                {
                    Console.WriteLine("Invalid format. Please enter a valid string.");
                }

            } while (!IsValidString(userPosition));


            DateTime userJoiningDate;

            do
            {
                Console.WriteLine("Enter your JoiningDate (yyyy-MM-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", null, DateTimeStyles.None, out userJoiningDate))
                {
                    break; // Exit the loop if the input is a valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                }

            } while (true);
            DateTime userTerminationDate;
            do
            {
                Console.WriteLine("Enter your TerminationDate (yyyy-MM-dd): ");
                string terminationInput = Console.ReadLine();



                if (DateTime.TryParseExact(terminationInput, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime tempTerminationDate))
                {
                    if (tempTerminationDate > userJoiningDate)
                    {
                        userTerminationDate = tempTerminationDate;
                        break; // Exit the loop if the termination date is valid
                    }
                    else
                    {
                        Console.WriteLine("Termination date must be greater than the Joining date.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format or leave it blank.");
                }

            } while (true);

            string Conformationmessage = employeeRepository.AddEmployee(userFirstName, userLastName, userDateOfBirth, userGender, userEmail, userPhoneNumber, userAddress, userPosition, userJoiningDate, userTerminationDate);
            Console.WriteLine(Conformationmessage);
        }
        public void UpdateEmployee()
        {
            try
            {
                Console.WriteLine("Enter your EmployeeID");
                int userEmployeeID = int.Parse(Console.ReadLine());
                if ((employeeRepository.GetEmployeeById(userEmployeeID)) != null)
                {
                    Console.WriteLine("Select Option To Update");
                    Console.WriteLine("1.FirstName");
                    Console.WriteLine("2.LastName");
                    Console.WriteLine("3.DateOfBirth");
                    Console.WriteLine("4.Gender");
                    Console.WriteLine("5.Emial");
                    Console.WriteLine("6.PhoneNumber");
                    Console.WriteLine("7.Address");
                    Console.WriteLine("8.Position");
                    Console.WriteLine("9.JoiningDate");
                    Console.WriteLine("10.Termination Date");
                    int userChoice = int.Parse(Console.ReadLine());
                    switch (userChoice)
                    {
                        case 1:
                            string userFirstName;
                            do
                            {
                                Console.WriteLine("Enter your FirstName: ");
                                userFirstName = Console.ReadLine();

                                if (!IsValidString(userFirstName))
                                {
                                    Console.WriteLine("Invalid format. Please enter a valid string.");
                                }

                            } while (!IsValidString(userFirstName));
                            string Conformationmessage = employeeRepository.UpdateFirstName(userFirstName, userEmployeeID);
                            Console.WriteLine(Conformationmessage);
                            break;
                        case 2:
                            string userLastName;
                            do
                            {
                                Console.WriteLine("Enter your LastName: ");
                                userLastName = Console.ReadLine();

                                if (!IsValidString(userLastName))
                                {
                                    Console.WriteLine("Invalid format. Please enter a valid string.");
                                }

                            } while (!IsValidString(userLastName));
                            string ConformationmessageLastName = employeeRepository.UpdateLastName(userLastName, userEmployeeID);
                            Console.WriteLine(ConformationmessageLastName);
                            break;
                        case 3:
                            DateTime userDateOfBirth;
                            do
                            {
                                Console.WriteLine("Enter your new DateOfBirth (yyyy-MM-dd): ");
                                string userInput = Console.ReadLine();

                                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out userDateOfBirth))
                                {
                                    break; // Exit the loop if the input is a valid date
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                                }

                            } while (true);

                            string ConformationmessageDateOfBirth = employeeRepository.UpdateDateOfBirth(userDateOfBirth, userEmployeeID);
                            Console.WriteLine(ConformationmessageDateOfBirth);
                            break;
                        case 4:
                            string userGender;
                            do
                            {
                                Console.WriteLine("Enter your Gender (male/female): ");
                                userGender = Console.ReadLine().ToLower(); // Convert input to lowercase for case-insensitive comparison

                                if (IsValidGender(userGender))
                                {
                                    break; // Exit the loop if the input is a valid gender
                                }
                                else
                                {
                                    Console.WriteLine("Invalid gender. Please enter a valid gender (male, female, or transgender).");
                                }

                            } while (true);
                            string ConformationmessageGender = employeeRepository.UpdateGender(userGender, userEmployeeID);
                            Console.WriteLine(ConformationmessageGender);

                            break;
                        case 5:
                            string userEmail;
                            do
                            {
                                Console.WriteLine("Enter your email: ");
                                userEmail = Console.ReadLine();

                                if (IsValidEmail(userEmail))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid email format or does not end with @gmail.com. Please enter a valid Gmail address.");
                                }
                            } while (true);
                            string ConformationmessageEmail = employeeRepository.UpdateEmail(userEmail, userEmployeeID);
                            Console.WriteLine(ConformationmessageEmail);


                            break;
                        case 6:
                            string userPhoneNumber;
                            do
                            {
                                Console.WriteLine("Enter your PhoneNumber: ");
                                userPhoneNumber = (Console.ReadLine());

                                if (IsValidPhoneNumber(userPhoneNumber))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid phone number format. Please enter a valid 10-digit phone number starting with +91 or 91.");
                                }
                            } while (true);
                            string ConformationmessagePhoneNumber = employeeRepository.UpdatePhoneNumber(userPhoneNumber, userEmployeeID);
                            Console.WriteLine(ConformationmessagePhoneNumber);

                            break;
                        case 7:
                            Console.WriteLine("Enter your new Address ");
                            string userAddress = Console.ReadLine();
                            string ConformationmessageAddress = employeeRepository.UpdateAddress(userAddress, userEmployeeID);
                            Console.WriteLine(ConformationmessageAddress);

                            break;
                        case 8:
                            Console.WriteLine("Enter your new Position ");
                            string userPosition = Console.ReadLine();
                            string ConformationmessagePosition = employeeRepository.UpdatePosition(userPosition, userEmployeeID);
                            Console.WriteLine(ConformationmessagePosition);

                            break;
                        case 9:
                            DateTime userJoiningDate;

                            do
                            {
                                Console.WriteLine("Enter your JoiningDate (yyyy-MM-dd): ");
                                string userInput = Console.ReadLine();

                                if (DateTime.TryParseExact(userInput, "yyyy-MM-dd", null, DateTimeStyles.None, out userJoiningDate))
                                {
                                    break; // Exit the loop if the input is a valid date
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format.");
                                }

                            } while (true);
                            string ConformationmessageJoiningdate = employeeRepository.UpdateJoiningDate(userJoiningDate, userEmployeeID);
                            Console.WriteLine(ConformationmessageJoiningdate);
                            break;

                        case 10:

                            DateTime userTerminationDate;
                            do
                            {
                                DateTime JoiningDate = employeeRepository.GetJoiningDate(userEmployeeID);
                                Console.WriteLine($"Joining Date :{JoiningDate}");
                                Console.WriteLine("Enter your TerminationDate (yyyy-MM-dd): ");
                                string terminationInput = Console.ReadLine();



                                if (DateTime.TryParseExact(terminationInput, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime tempTerminationDate))
                                {
                                    if (tempTerminationDate > JoiningDate)
                                    {
                                        userTerminationDate = tempTerminationDate;
                                        break; // Exit the loop if the termination date is valid
                                    }
                                    else
                                    {
                                        Console.WriteLine("Termination date must be greater than the Joining date.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Please enter a valid date in yyyy-MM-dd format or leave it blank.");
                                }

                            } while (true);

                            string ConformationmessageterminationDate = employeeRepository.UpdateTerminationDate(userTerminationDate, userEmployeeID);
                            Console.WriteLine(ConformationmessageterminationDate);
                            break;
                        default:
                            Console.WriteLine("You Selected Wrong Choice Please Check it");
                            break;



                    }

                }
                else
                {
                    throw new EmployeeNotFoundException();
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void RemoveEmployee()
        {
            Console.WriteLine("Enter your EmployeeID");
            int userEmployeeID = int.Parse(Console.ReadLine());
            string employeedremoved = employeeRepository.RemoveEmployee(userEmployeeID);
            Console.WriteLine(employeedremoved);


        }
    }
}
