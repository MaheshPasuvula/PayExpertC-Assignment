using CaseStudyAssignment.DataBaseUtil;
using CaseStudyAssignment.Exceptions;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Repository
{
    internal class FinancialRecordRepository : IFinancialRecordRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public FinancialRecordRepository()
        {
            sqlconnection = new SqlConnection(DataBaseContext.GetConnectionString());
            cmd = new SqlCommand();
        }
        public string GetAllFinancialRecords()
        {
            try
            {
                cmd.CommandText = "SELECT * FROM FINANCIALRECORD";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    var table = new ConsoleTable("RECORDID", "EMPLOYEEID", "RECORDDATE", "DESCRIPTION", "AMOUNT", "RECORDTYPE");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["RECORDID"],
                            reader["EMPLOYEEID"],
                            reader["RECORDDATE"],
                            reader["DESCRIPTION"],
                            reader["AMOUNT"],
                            reader["RECORDTYPE"]

                        );
                    }
                    // Capture Console output to a StringWriter
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;

                }
                else
                {
                    throw new FinancialRecordException();
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Error:{ex.Message} Record Not Found");
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

        public string AddFinancialRecord(int userEmployeeId, string userDescription, decimal userAmount, string userRecordType)
        {
            string Conformationmessage = null;

            try
            {
                cmd.CommandText = "INSERT INTO FINANCIALRECORD(EMPLOYEEID,RECORDDATE,DESCRIPTION,AMOUNT,RECORDTYPE)" +
                    "VALUES(@EMPLOYEEID,@RECORDDATE,@DESCRIPTION,@AMOUNT,@RECORDTYPE)";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeId);
                cmd.Parameters.AddWithValue("@RECORDDATE", DateTime.Now);
                cmd.Parameters.AddWithValue("@DESCRIPTION", userDescription);
                cmd.Parameters.AddWithValue("@AMOUNT", userAmount);
                cmd.Parameters.AddWithValue("@RECORDTYPE", userRecordType);





                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Conformationmessage = $"Dear Employee,\n\n"
                       + "Your  RecordDetails Are Received \n\n"
                      + $"EmployeeID:{userEmployeeId}\n\n"
                      + $"RecordDate:{DateTime.Now}\n\n"
                      + $"Description:{userDescription}\n\n"
                      + $"Amount:{userAmount}\n\n"
                       + $"RecordType:{userRecordType}\n\n"
                       + $"Thank You";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Check if the exception is a foreign key violation
                {
                    Console.WriteLine($"Foreign key violation: Employee with ID {userEmployeeId} does not exist.");
                }
                if (ex.Number == -2) // Assuming -2 is the error number for timeout
                {
                    Console.WriteLine("SQL Timeout Exception occurred: " + ex.Message);
                    // Implement retry logic, notify the user, log the error, etc.
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
            return Conformationmessage;

        }
        public string GetFinancialRecordById(int userRecordId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM FINANCIALRECORD WHERE RECORDID=@RECORDID";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RECORDID", userRecordId);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    var table = new ConsoleTable("RECORDID", "EMPLOYEEID", "RECORDDATE", "DESCRIPTION", "AMOUNT", "RECORDTYPE");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["RECORDID"],
                            reader["EMPLOYEEID"],
                            reader["RECORDDATE"],
                            reader["DESCRIPTION"],
                            reader["AMOUNT"],
                            reader["RECORDTYPE"]

                        );
                    }
                    // Capture Console output to a StringWriter
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;

                }
                else
                {
                    throw new FinancialRecordException();
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Error:{ex.Message} Record Not Found");
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
        public string GetFinancialRecordsForEmployee(int userEmployeeId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM FINANCIALRECORD WHERE EMPLOYEEID=@EMPLOYEEID";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeId);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    var table = new ConsoleTable("RECORDID", "EMPLOYEEID", "RECORDDATE", "DESCRIPTION", "AMOUNT", "RECORDTYPE");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["RECORDID"],
                            reader["EMPLOYEEID"],
                            reader["RECORDDATE"],
                            reader["DESCRIPTION"],
                            reader["AMOUNT"],
                            reader["RECORDTYPE"]

                        );
                    }
                    // Capture Console output to a StringWriter
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;

                }
                else
                {
                    throw new FinancialRecordException();
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Error:{ex.Message} Record Not Found");
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
        public string GetFinancialRecordsForDate(DateTime userRecordDate)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM FINANCIALRECORD WHERE RECORDDATE=@RECORDDATE";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RECORDDATE", userRecordDate);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    var table = new ConsoleTable("RECORDID", "EMPLOYEEID", "RECORDDATE", "DESCRIPTION", "AMOUNT", "RECORDTYPE");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["RECORDID"],
                            reader["EMPLOYEEID"],
                            reader["RECORDDATE"],
                            reader["DESCRIPTION"],
                            reader["AMOUNT"],
                            reader["RECORDTYPE"]

                        );
                    }
                    // Capture Console output to a StringWriter
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;

                }
                else
                {
                    throw new FinancialRecordException();
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine($"Error:{ex.Message} Record Not Found");
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


    }
}
