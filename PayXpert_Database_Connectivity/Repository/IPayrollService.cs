using PayXpert_Database_Connectivity.Models;

namespace PayXpert_Database_Connectivity.Repository
{
    public interface IPayrollService
    {
        void GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate, decimal basicSalary, decimal deductions, decimal overtimePay);
        PayRoll GetPayrollById(int payrollId);
        List<PayRoll> GetPayrollsForEmployee(int employeeId);
        List<PayRoll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}
