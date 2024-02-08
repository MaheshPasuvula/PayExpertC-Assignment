using CaseStudyAssignment.DataBaseUtil;
using CaseStudyAssignment.Exceptions;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Repository
{
    public class TaxRepository : ITaxRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public TaxRepository()
        {
            sqlconnection = new SqlConnection(DataBaseContext.GetConnectionString());
            cmd = new SqlCommand();
        }
        public string GetAllTaxes()
        {
            try
            {

                cmd.CommandText = "SELECT * FROM TAX";
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                var table = new ConsoleTable("TAXID", "EMPLOYEEID", "TAXYEAR", "TAXABLEINCOME", "TAXAMOUNT");
                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["TAXID"],
                            reader["EMPLOYEEID"],
                            reader["TAXYEAR"],
                            reader["TAXABLEINCOME"],
                            reader["TAXAMOUNT"]

                        );

                        // Capture Console output to a StringWriter

                    }
                        string tableContent = table.ToString();

                        // Return the table content as a string
                        return tableContent;
                }
                else
                {
                    throw new TaxCalculationException();
                }


            }

            catch (TaxCalculationException ex)
            {
                Console.WriteLine($"Error:{ex.Message} ");
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
        public string CalculateTax(int userEmployeeId, int userYear)
        {
            string calculateTaxres = null;
            try
            {
                cmd.CommandText = "SELECT TAXABLEINCOME FROM TAX WHERE EMPLOYEEID=@EMPLOYEEID AND TAXYEAR=@TAXYEAR";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeId);
                cmd.Parameters.AddWithValue("@TAXYEAR", userYear);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                decimal taxableIncome = 0;
                decimal taxAmount = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        taxableIncome = Convert.ToDecimal(reader["TAXABLEINCOME"]);


                        taxAmount = 0.10m * taxableIncome;
                        calculateTaxres = $"Dear User \n\n"
                                          + $"Your TaxCalculation :\n\n"
                                          + $"TaxableIncome:{taxableIncome}\n"
                                          + $"TaxAmount:{taxAmount}\n\n"
                                          + $"Thank you";

                    }


                }

                else
                {
                    throw new TaxCalculationException();
                }
            }
            catch (TaxCalculationException ex)
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
            return calculateTaxres;
        }
        public string GetTaxById(int userTaxId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM TAX WHERE TAXID=@TAXID";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TAXID", userTaxId);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var table = new ConsoleTable("TAXID", "EMPLOYEEID", "TAXYEAR", "TAXABLEINCOME", "TAXAMOUNT");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["TAXID"],
                            reader["EMPLOYEEID"],
                            reader["TAXYEAR"],
                            reader["TAXABLEINCOME"],
                            reader["TAXAMOUNT"]

                        );
                    }
                    // Capture Console output to a StringWriter
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;

                }
                else
                {
                    throw new TaxCalculationException();
                }
            }

            catch (TaxCalculationException ex)
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
        public string GetTaxesForEmployee(int userEmployeeId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM TAX WHERE EMPLOYEEID=@EMPLOYEEID";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeId);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var table = new ConsoleTable("TAXID", "EMPLOYEEID", "TAXYEAR", "TAXABLEINCOME", "TAXAMOUNT");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["TAXID"],
                            reader["EMPLOYEEID"],
                            reader["TAXYEAR"],
                            reader["TAXABLEINCOME"],
                            reader["TAXAMOUNT"]

                        );
                    }
                    // Capture Console output to a StringWriter
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;

                }
                else
                {
                    throw new EmployeeNotFoundException();
                }
            }

            catch (TaxCalculationException ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
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
        public string GetTaxesForYear(int userYear)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM TAX WHERE TAXYEAR=@TAXYEAR";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TAXYEAR", userYear);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var table = new ConsoleTable("TAXID", "EMPLOYEEID", "TAXYEAR", "TAXABLEINCOME", "TAXAMOUNT");

                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["TAXID"],
                            reader["EMPLOYEEID"],
                            reader["TAXYEAR"],
                            reader["TAXABLEINCOME"],
                            reader["TAXAMOUNT"]

                        );
                    }
                    // Capture Console output to a StringWriter
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;

                }
                else
                {
                    throw new TaxCalculationException();
                }
            }

            catch (TaxCalculationException ex)
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
        public KeyValuePair<decimal, decimal> CalculateTaxHighIncome()
        {
            decimal maxTaxableIncome = 0;
            decimal maxTaxAmount = 0;

            try
            {
                cmd.CommandText = "SELECT MAX(TAXABLEINCOME), MAX(TAXAMOUNT) FROM TAX";
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    maxTaxableIncome = reader.GetDecimal(0);
                    maxTaxAmount = reader.GetDecimal(1);
                }
                else
                {
                    throw new EmployeeNotFoundException();
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
            return new KeyValuePair<decimal, decimal>(maxTaxableIncome, maxTaxAmount);


        }
    }
}
