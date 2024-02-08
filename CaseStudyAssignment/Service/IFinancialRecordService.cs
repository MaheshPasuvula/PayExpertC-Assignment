using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAssignment.Service
{
    internal interface IFinancialRecordService
    {
        void AddFinancialRecord();
        void GetFinancialRecordById();
        void GetFinancialRecordsForEmployee();
        void GetFinancialRecordsForDate();
        void GetAllFinancialRecords();
    }
}
