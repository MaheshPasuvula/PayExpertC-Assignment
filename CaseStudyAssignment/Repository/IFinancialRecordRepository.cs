using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Repository
{
    internal interface IFinancialRecordRepository
    {
        string GetAllFinancialRecords();
        string AddFinancialRecord(int userEmployeeId, string userDescription, decimal userAmount, string userRecordType);
        string  GetFinancialRecordById(int userRecordId);
        string GetFinancialRecordsForEmployee( int userEmployeeId);
        string  GetFinancialRecordsForDate(DateTime userRecordDate);
    }
}
