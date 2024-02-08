using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CaseStudyAssignment.DataBaseUtil;
using CaseStudyAssignment.Exceptions;
using ConsoleTables;

namespace CaseStudyAssignment.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public EmployeeRepository()
        {
            sqlconnection = new SqlConnection(DataBaseContext.GetConnectionString());
            cmd = new SqlCommand();
        }
        private bool EmployeeExists(int employeeID)
        {
            // Check if the employee exists in your database using a SELECT query
            // Return true if exists, false otherwise
            // You should replace this with your actual database logic

            cmd.CommandText = "SELECT COUNT(*) FROM EMPLOYEE WHERE EMPLOYEEID=@EMPLOYEEID";
            cmd.Parameters.AddWithValue("@EMPLOYEEID", employeeID);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();


            int count = (int)cmd.ExecuteScalar();
            sqlconnection.Close();
            return count > 0;


        }
        public string GetEmployeeById(int userEmployeeID)
        {

            cmd.CommandText = "SELECT * FROM EMPLOYEE WHERE EMPLOYEEID=@EMPLOYEEID";
            cmd.Connection = sqlconnection;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeID);
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                var table = new ConsoleTable("EMPLOYEEID", "FIRSTNAME", "LASTNAME", "DATEOFBIRTH", "GENDER", "EMAIL", "PHONENUMBER", "ADDRESS", "POSITION", "JOININGDATE", "TERMINATIONDATE");

                while (reader.Read())
                {
                    // Add rows to the ConsoleTable
                    table.AddRow(
                        reader["EMPLOYEEID"],
                        reader["FIRSTNAME"],
                        reader["LASTNAME"],
                        reader["DATEOFBIRTH"],
                        reader["GENDER"],
                        reader["EMAIL"],
                        reader["PHONENUMBER"],
                        reader["ADDRESS"],
                        reader["POSITION"],
                        reader["JOININGDATE"],
                        reader["TERMINATIONDATE"]
                    );
                }
                // Capture Console output to a StringWriter
                string tableContent = table.ToString();

                // Return the table content as a string
                sqlconnection.Close();

                return tableContent;

            }
            else
            {
                throw new EmployeeNotFoundException();
            }








        }
        public string GetAllEmployees()
        {
            try
            {

                cmd.CommandText = "SELECT * FROM EMPLOYEE";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var table = new ConsoleTable("EMPLOYEEID", "FIRSTNAME", "LASTNAME", "DATEOFBIRTH", "GENDER", "EMAIL", "PHONENUMBER", "ADDRESS", "POSITION", "JOININGDATE", "TERMINATIONDATE");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["EMPLOYEEID"],
                            reader["FIRSTNAME"],
                            reader["LASTNAME"],
                            reader["DATEOFBIRTH"],
                            reader["GENDER"],
                            reader["EMAIL"],
                            reader["PHONENUMBER"],
                            reader["ADDRESS"],
                            reader["POSITION"],
                            reader["JOININGDATE"],
                            reader["TERMINATIONDATE"]
                        );
                    }
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;
                }
                else
                {
                    throw new EmployeeNotFoundException();
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
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
            finally
            {
                sqlconnection.Close();
            }

            return null;

        }
        public string AddEmployee(string userFirstName, string userLastName, DateTime userDateOfBirth, string userGender, string userEmail, string userPhoneNumber, string userAddress, string userPosition, DateTime userJoiningDate, DateTime userTerminationDate)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "INSERT INTO EMPLOYEE(FIRSTNAME,LASTNAME,DATEOFBIRTH,GENDER,EMAIL,PHONENUMBER,ADDRESS,POSITION,JOININGDATE,TERMINATIONDATE )" +
                    "values(@FIRSTNAME,@LASTNAME,@DATEOFBIRTH,@GENDER,@EMAIL,@PHONENUMBER,@ADDRESS,@POSITION,@JOININGDATE,@TERMINATIONDATE)";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FIRSTNAME", userFirstName);
                cmd.Parameters.AddWithValue("@LASTNAME", userLastName);
                cmd.Parameters.AddWithValue("@DATEOFBIRTH", userDateOfBirth);
                cmd.Parameters.AddWithValue("@GENDER", userGender);
                cmd.Parameters.AddWithValue("@EMAIL", userEmail);
                cmd.Parameters.AddWithValue("@PHONENUMBER", userPhoneNumber);
                cmd.Parameters.AddWithValue("@ADDRESS", userAddress);
                cmd.Parameters.AddWithValue("@POSITION", userPosition);
                cmd.Parameters.AddWithValue("@JOININGDATE", userJoiningDate);
                cmd.Parameters.AddWithValue("@TERMINATIONDATE", userJoiningDate);

                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Conformationmessage = $"Dear Employee,\n\n"
                        + "Your Details Are Received \n\n"
                       + $"FirstName:{userFirstName}\n\n"
                       + $"LastName:{userLastName}\n\n"
                       + $"DateOfBirth:{userDateOfBirth}\n\n"
                       + $"Gender:{userGender}\n\n"
                        + $"Email:{userEmail}\n\n"
                       + $"PhoneNumber:{userPhoneNumber}\n\n"
                       + $"Address:{userAddress}\n\n"
                        + $"Position:{userPosition}\n\n"
                        + $"JoiningDate:{userJoiningDate}\n\n"
                         + $"TerminationDate:{userJoiningDate}\n\n"
                         + $"Thank You";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    Console.WriteLine("Error: Duplicate key violation. Please check the entered data.");
                    Conformationmessage = "Error: Duplicate key violation. Please check the entered data.";
                }
                if (ex.Number == -2) // Assuming -2 is the error number for timeout
                {
                    Console.WriteLine("SQL Timeout Exception occurred: " + ex.Message);
                    // Implement retry logic, notify the user, log the error, etc.
                }

                else
                {
                    throw ex;
                }
            }



            finally
            {
                sqlconnection.Close();
            }
            return Conformationmessage;
        }
        public string UpdateFirstName(string userFirstName, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET FirstName=@FIRSTNAME where EMPLOYEEID=@EMPLOYEEIDFN";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FIRSTNAME", userFirstName);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDFN", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                             + $"FirstName is Updated to: {userFirstName}\n"

                                            + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }



            return Conformationmessage;
        }
        public string UpdateLastName(string userLastName, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET lastName=@LASTNAME where EMPLOYEEID=@EMPLOYEEIDLN";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LASTNAME", userLastName);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDLN", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                            + $"LastName is Updated to: {userLastName}\n"

                                           + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }



            return Conformationmessage;

        }
        public string UpdateDateOfBirth(DateTime userDateOfBirth, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET DATEOFBIRTH=@DATEOFBIRTH where EMPLOYEEID=@EMPLOYEEIDDB";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DATEOFBIRTH", userDateOfBirth);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDDB", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                            + $"DateofBirth is Updated to: {userDateOfBirth}\n"

                                           + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }



            return Conformationmessage;

        }
        public string UpdateGender(string userGender, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET GENDER=@GENDER where EMPLOYEEID=@EMPLOYEEIDG";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@GENDER", userGender);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDG", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                            + $"Gender is Updated to: {userGender}\n"

                                           + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }



            return Conformationmessage;
        }
        public string UpdateEmail(string userEmail, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET EMAIL=@EMAIL where EMPLOYEEID=@EMPLOYEEIDEM";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMAIL", userEmail);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDEM", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                            + $"Email is Updated to: {userEmail}\n"

                                           + "Sincerely,\nFlipKart\nflipkart@gmail.com";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    Console.WriteLine("Error: Duplicate key violation. Please check the entered data.");
                }
                if (ex.Number == -2) // Assuming -2 is the error number for timeout
                {
                    Console.WriteLine("SQL Timeout Exception occurred: " + ex.Message);
                    // Implement retry logic, notify the user, log the error, etc.
                }

                else
                {
                    throw ex;
                }
            }
            finally
            {
                sqlconnection.Close();
            }
            return Conformationmessage;
        }
        public string UpdatePhoneNumber(string userPhoneNumber, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET PHONENUMBER=@PHONENUMBER where EMPLOYEEID=@EMPLOYEEIDPH";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PHONENUMBER", userPhoneNumber);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDPH", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                            + $"PhoneNumber is Updated to: {userPhoneNumber}\n"

                                           + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }



            return Conformationmessage;
        }
        public string UpdateAddress(string userAddress, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET ADDRESS=@ADDRESS where EMPLOYEEID=@EMPLOYEEIDUA";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ADDRESS", userAddress);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDUA", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                            + $"Address is Updated to: {userAddress}\n"

                                           + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }



            return Conformationmessage;
        }
        public string UpdatePosition(string userPosition, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET POSITION=@POSITION where EMPLOYEEID=@EMPLOYEEIDUP";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@POSITION", userPosition);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDUP", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                             + $"Position is Updated to: {userPosition}\n"

                                            + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }



            return Conformationmessage;
        }
        public string UpdateJoiningDate(DateTime userJoiningDate, int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET JOININGDATE=@JOININGDATE where EMPLOYEEID=@EMPLOYEEIDJD";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@JOININGDATE", userJoiningDate);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDJD", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                                            + $"JoiningDate is Updated to: {userJoiningDate}\n"

                                           + "Sincerely,\nFlipKart\nflipkart@gmail.com";
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
            finally
            {
                sqlconnection.Close();
            }
            return Conformationmessage;
        }
        public DateTime GetJoiningDate(int userEmployeeID)
        {
            DateTime joiningDate = DateTime.MinValue;
            try
            {
                cmd.CommandText = "SELECT JOININGDATE FROM EMPLOYEE where EMPLOYEEID=@EMPLOYEEIDJp";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEIDJp", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    joiningDate = (DateTime)reader["JOININGDATE"];

                }
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
            finally
            {
                sqlconnection.Close();
            }



            return joiningDate;

        }
        public string UpdateTerminationDate(DateTime userTerminationDate, int userEmployeeID)
        {
            try
            {
                cmd.CommandText = "UPDATE EMPLOYEE SET TERMINATIONDATE=@TERMINATIONDATE where EMPLOYEEID=@EMPLOYEEIDTREMDATE";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TERMINATIONDATE", userTerminationDate);
                cmd.Parameters.AddWithValue("@EMPLOYEEIDTREMDATE", userEmployeeID);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                String Conformationmessage = $"Dear Employee,\n\n"
                                             + $"TerminationDate is Updated to: {userTerminationDate}\n"

                                            + "Sincerely,\nFlipKart\nflipkart@gmail.com";
                sqlconnection.Close();
                return Conformationmessage;
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
            finally
            {
                sqlconnection.Close();
            }
            return null;

        }
        public string RemoveEmployee(int userEmployeeID)
        {
            String Conformationmessage = null;
            try
            {
                if (EmployeeExists(userEmployeeID))
                {
                    cmd.CommandText = "DELETE FROM EMPLOYEE  where EMPLOYEEID=@EMPLOYEEIDREM";
                    cmd.Connection = sqlconnection;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EMPLOYEEIDREM", userEmployeeID);
                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Conformationmessage = $"Dear Employee,\n\n"
                                                + $"Employee is Deleted with {userEmployeeID}\n"

                                               + "Sincerely,\nFlipKart\nflipkart@gmail.com";
                }
                else
                {
                    throw new EmployeeNotFoundException();
                }


            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"Error:{ex.Message}");

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
            finally
            {
                sqlconnection.Close();
            }
            return Conformationmessage;
        }


    }
}


