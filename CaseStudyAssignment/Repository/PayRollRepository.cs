using CaseStudyAssignment.DataBaseUtil;
using CaseStudyAssignment.Exceptions;
using CaseStudyAssignment.Model;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Repository
{
    public class PayRollRepository : IPayRollRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public PayRollRepository()
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
        public bool EmployeeExistsInPayroll(int userEmployeeID)
        {

            cmd.CommandText = "SELECT COUNT(*) FROM PAYROLL WHERE EMPLOYEEID = @EMPLOYEEID";
            cmd.Connection = sqlconnection;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeID);

            sqlconnection.Open();
            int count = (int)cmd.ExecuteScalar();
            sqlconnection.Close();

            return count > 0;
        }
        public string GeneratePayRoll(int userEmployeeID, DateTime userStartDate, DateTime userEndDate)
        {
            try
            {
                if (EmployeeExists(userEmployeeID) == false)
                {
                    throw new EmployeeNotFoundException();
                }
                else
                {
                    cmd.CommandText = "INSERT INTO PAYROLL(EMPLOYEEID,PAYPERIODSTARTDATE,PAYPERIODENDDATE,BASICSALARY,OVERTIMEPAY,DEDUCTIONS,NETSALARY)" +
                        "VALUES(@EMPLOYEEID,@PAYPERIODSTARTDATE,@PAYPERIODENDDATE,@BASICSALARY,@OVERTIMEPAY,@DEDUCTIONS,@NETSALARY)";
                    cmd.Connection = sqlconnection;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeID);
                    cmd.Parameters.AddWithValue("@PAYPERIODSTARTDATE", userStartDate);
                    cmd.Parameters.AddWithValue("@PAYPERIODENDDATE", userEndDate);
                    decimal basicSalary = GetUserBasicSalary();
                    cmd.Parameters.AddWithValue("@BASICSALARY", basicSalary);
                    decimal overTimePay = GetUserOverTimePay();
                    cmd.Parameters.AddWithValue("@OVERTIMEPAY", overTimePay);

                    decimal deduction = GetUserDeduction();
                    cmd.Parameters.AddWithValue("@DEDUCTIONS", deduction);

                    decimal netSalary = (basicSalary + overTimePay) - (deduction);
                    cmd.Parameters.AddWithValue("@NETSALARY", netSalary);



                    sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();



                    string result = $"NetSalary={netSalary}\n\n"
                                   + $"Inserted Successfully\n\n"
                                   + $"Thank You";

                    return result;
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
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                sqlconnection.Close();
            }
            return null;

        }

        private decimal GetUserOverTimePay()
        {
            decimal userOverTimePay;
            Console.WriteLine("Enter Your OverTimePay ");
            userOverTimePay = Convert.ToDecimal(Console.ReadLine());
            return userOverTimePay;
        }

        private decimal GetUserDeduction()
        {
            decimal userDeduction;
            Console.WriteLine("Enter Your Deduction ");
            userDeduction = Convert.ToDecimal(Console.ReadLine());
            return userDeduction;
        }

        private decimal GetUserBasicSalary()
        {
            decimal userBasicSalary;
            Console.WriteLine("Enter Your Basic Salary ");
            userBasicSalary = Convert.ToDecimal(Console.ReadLine());
            return userBasicSalary;
        }

        public string GenerateAllPayRolls()
        {
            try
            {
                cmd.CommandText = "SELECT * FROM PAYROLL ";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {


                    var table = new ConsoleTable("PAYROLLID", "EMPLOYEEID", "PAYPERIODSTARTDATE", "PAYPERIODENDDATE", "BASICSALARY", "OVERTIMEPAY", "DEDUCTIONS", "NETSALARY");
                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["PAYROLLID"],
                            reader["EMPLOYEEID"],
                            reader["PAYPERIODSTARTDATE"],
                            reader["PAYPERIODENDDATE"],
                            reader["BASICSALARY"],
                            reader["OVERTIMEPAY"],
                            reader["DEDUCTIONS"],
                            reader["NETSALARY"]

                        );
                    }
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;
                }
                else
                {
                    throw new PayRollGenerationException();
                }
            }
            catch (PayRollGenerationException ex)
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
            finally
            {

                sqlconnection.Close();
            }
            return null;
        }
        public string GetPayRollById(int userPayRollId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM PAYROLL WHERE PAYROLLID=@PAYROLLID ";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAYROLLID", userPayRollId);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var table = new ConsoleTable("PAYROLLID", "EMPLOYEEID", "PAYPERIODSTARTDATE", "PAYPERIODENDDATE", "BASICSALARY", "OVERTIMEPAY", "DEDUCTIONS", "NETSALARY");
                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["PAYROLLID"],
                            reader["EMPLOYEEID"],
                            reader["PAYPERIODSTARTDATE"],
                            reader["PAYPERIODENDDATE"],
                            reader["BASICSALARY"],
                            reader["OVERTIMEPAY"],
                            reader["DEDUCTIONS"],
                            reader["NETSALARY"]

                        );
                    }
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;
                }
                else
                {
                    throw new PayRollGenerationException();
                }
            }
            catch (PayRollGenerationException ex)
            {
                Console.WriteLine($"Error :{ex.Message}");
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
        public string GetPayRollsByEmployee(int userEmployeeId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM PAYROLL WHERE EMPLOYEEID=@EMPLOYEEID ";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeId);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    var table = new ConsoleTable("PAYROLLID", "EMPLOYEEID", "PAYPERIODSTARTDATE", "PAYPERIODENDDATE", "BASICSALARY", "OVERTIMEPAY", "DEDUCTIONS", "NETSALARY");
                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["PAYROLLID"],
                            reader["EMPLOYEEID"],
                            reader["PAYPERIODSTARTDATE"],
                            reader["PAYPERIODENDDATE"],
                            reader["BASICSALARY"],
                            reader["OVERTIMEPAY"],
                            reader["DEDUCTIONS"],
                            reader["NETSALARY"]

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
        public string GetPayrollsForPeriod(DateTime userStartDate, DateTime userEndDate)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM PAYROLL WHERE PAYPERIODSTARTDATE>=@PAYPERIODSTARTDATE AND PAYPERIODENDDATE<=@PAYPERIODENDDATE";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAYPERIODSTARTDATE", userStartDate);
                cmd.Parameters.AddWithValue("@PAYPERIODENDDATE", userEndDate);
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var table = new ConsoleTable("PAYROLLID", "EMPLOYEEID", "PAYPERIODSTARTDATE", "PAYPERIODENDDATE", "BASICSALARY", "OVERTIMEPAY", "DEDUCTIONS", "NETSALARY");
                    while (reader.Read())
                    {
                        // Add rows to the ConsoleTable
                        table.AddRow(
                            reader["PAYROLLID"],
                            reader["EMPLOYEEID"],
                            reader["PAYPERIODSTARTDATE"],
                            reader["PAYPERIODENDDATE"],
                            reader["BASICSALARY"],
                            reader["OVERTIMEPAY"],
                            reader["DEDUCTIONS"],
                            reader["NETSALARY"]

                        );
                    }
                    string tableContent = table.ToString();

                    // Return the table content as a string
                    return tableContent;
                }
                else
                {
                    throw new PayRollGenerationException();
                }
            }
            catch (PayRollGenerationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
        public decimal CalculateGrossSalary(int userEmployeeID, DateTime userStartDate, DateTime userEndDate)

        {
            try
            {
                cmd.CommandText = "SELECT BASICSALARY FROM PAYROLL WHERE EMPLOYEEID=@EMPLOYEEID AND (PAYPERIODSTARTDATE=@STARTDATE AND PAYPERIODENDDATE=@ENDDATE )";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeID);
                cmd.Parameters.AddWithValue("@STARTDATE", userStartDate);
                cmd.Parameters.AddWithValue("@ENDDATE", userEndDate);


                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    reader.Read(); // Assuming only one row is returned
                    decimal basicSalary = reader.GetDecimal(0); // Assuming BASICSALARY is the first column
                    return basicSalary;
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
            return 0;
        }
        public decimal CalculateNetSalary(int userEmployeeID, DateTime userStartDate, DateTime userEndDate)
        {
            try
            {
                cmd.CommandText = "SELECT (((BASICSALARY)+(OVERTIMEPAY))-(DEDUCTIONS))AS NETSALARY FROM PAYROLL WHERE EMPLOYEEID=@EMPLOYEEID AND (PAYPERIODSTARTDATE=@STARTDATE AND PAYPERIODENDDATE=@ENDDATE ) ";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeID);
                cmd.Parameters.AddWithValue("@STARTDATE", userStartDate);
                cmd.Parameters.AddWithValue("@ENDDATE", userEndDate);


                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    reader.Read(); // Assuming only one row is returned
                    decimal netSalary = reader.GetDecimal(0); // Assuming NETSALARY is the first column
                    return netSalary;
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
            return 0;
        }
        public List<Payroll> GetPayrollRecords(int userEmployeeID, DateTime userStartDate, DateTime userEndDate)
        {
            List<Payroll> payrollRecords = new List<Payroll>();
            try
            {
                cmd.CommandText = "SELECT PAYROLLID, EMPLOYEEID, PAYPERIODSTARTDATE, PAYPERIODENDDATE, BASICSALARY, OVERTIMEPAY, DEDUCTIONS, NETSALARY " +
                                  "FROM PAYROLL WHERE EMPLOYEEID = @EMPLOYEEID AND PAYPERIODSTARTDATE = @STARTDATE AND PAYPERIODENDDATE = @ENDDATE";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EMPLOYEEID", userEmployeeID);
                cmd.Parameters.AddWithValue("@STARTDATE", userStartDate);
                cmd.Parameters.AddWithValue("@ENDDATE", userEndDate);

                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Payroll payrollRecord = new Payroll
                    {
                        PayrollID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        PayPeriodStartDate = reader.GetDateTime(2),
                        PayPeriodEndDate = reader.GetDateTime(3),
                        BasicSalary = reader.GetDecimal(4),
                        OverTimePay = reader.GetDecimal(5),
                        Deductions = reader.GetDecimal(6),
                        NetSalary = reader.GetDecimal(7)
                    };
                    payrollRecords.Add(payrollRecord);
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
            return payrollRecords;
        }




    }
}
