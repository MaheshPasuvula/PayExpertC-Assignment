using CaseStudyAssignment.Repository;
using CaseStudyAssignment.Exceptions;
using CaseStudyAssignment.Model;
using System.Data.SqlClient;
namespace PayRollExpertTest
{
    public class CaseStudyAssignmentTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_GrossSalaryTest()
        {
            PayRollRepository payRollRepository = new PayRollRepository();
            decimal expectedgrosssalary = 220000;
            decimal actualgrosssalary = payRollRepository.CalculateGrossSalary(3, new DateTime(2020, 04, 15), new DateTime(2021, 04, 15));
            Assert.That(actualgrosssalary, Is.EqualTo(expectedgrosssalary));
        }
        [Test]
        public void Test_NetSalaryTest()
        {
            PayRollRepository payRollRepository = new PayRollRepository();
            decimal expectednetsalary = 488400;
            decimal actualnetsalary = payRollRepository.CalculateNetSalary(4, new DateTime(2020, 02, 15), new DateTime(2021, 02, 15));
            Assert.That(actualnetsalary, Is.EqualTo(expectednetsalary));
        }
        [Test]
        public void Test_CalculateTaxHighIncome()
        {
            TaxRepository taxRepository = new TaxRepository();
            decimal expectedHighIncome = 600000;
            decimal expectedHighTaxAmount = 12000;
            var actualresult = taxRepository.CalculateTaxHighIncome();
            Assert.IsNotNull(actualresult);
            Assert.That(actualresult.Key, Is.EqualTo(expectedHighIncome)); // Check max taxable income
            Assert.That(actualresult.Value, Is.EqualTo(expectedHighTaxAmount));


        }

        [Test]
        [TestCaseSource(nameof(SourceProvider))]
        public void TestToProcessPayrollForMultipleEmployees(int payrollId, int employeeId, DateTime startDate, DateTime endDate, decimal basicSalary, decimal overtimePay, decimal deductions, decimal netSalary, int expectedRecords)
        {
            // Arrange
            PayRollRepository payrollRepository = new PayRollRepository();

            // Act
            List<Payroll> payrollRecords = payrollRepository.GetPayrollRecords(employeeId, startDate, endDate);

            // Assert
            foreach (var payrollRecord in payrollRecords)
            {
                Assert.AreEqual(payrollId, payrollRecord.PayrollID);
                Assert.AreEqual(employeeId, payrollRecord.EmployeeID);
                Assert.AreEqual(startDate, payrollRecord.PayPeriodStartDate);
                Assert.AreEqual(endDate, payrollRecord.PayPeriodEndDate);
                Assert.AreEqual(basicSalary, payrollRecord.BasicSalary);
                Assert.AreEqual(overtimePay, payrollRecord.OverTimePay);
                Assert.AreEqual(deductions, payrollRecord.Deductions);
                Assert.AreEqual(netSalary, payrollRecord.NetSalary);
            }
        }

        public static IEnumerable<object[]> SourceProvider()
        {
            yield return new object[] { 100, 2, DateTime.Parse("2023-01-05"), DateTime.Parse("2024-01-15"), 150000m, 5000m, 21600m, 133400m, 1 };
            yield return new object[] { 101, 3, DateTime.Parse("2020-04-15"), DateTime.Parse("2021-04-15"), 220000m, 15000m, 21600m, 213400m, 1 };
        }

        [Test]
        public void Test_Verify_ErrorHandlingForInvalidEmployeeData()
        {

            EmployeeRepository employeeRepository = new EmployeeRepository();
            // Ensure that the system handles the invalid employee data gracefully
            Assert.Throws<EmployeeNotFoundException>(() => employeeRepository.GetEmployeeById(345));
        }

    }
}