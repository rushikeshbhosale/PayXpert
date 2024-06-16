using PayXpert_Database_Connectivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Repository
{
    public interface IFinancialRecordService
    {
        int AddFinancialRecord(int employeeId, string description, decimal amount, string recordType);
        FinancialRecord GetFinancialRecordById(int recordId);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
    }
}
