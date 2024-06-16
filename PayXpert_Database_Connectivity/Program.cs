using PayXpert_Database_Connectivity.App;
using PayXpert_Database_Connectivity.Repository;
using PayXpert_Database_Connectivity.Services;

namespace PayXpert_Database_Connectivity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEmployeeService employeeRepository = new EmployeeRepository();
            IPayrollService payrollRepository = new PayrollRepository();
            IFinancialRecordService financialRecordRepository = new FinancialRecordRepository();
            ITaxService taxRepository = new TaxRepository();

            EmployeeService employeeService = new EmployeeService(employeeRepository);
            FinancialRecordService financialRecordService = new FinancialRecordService(financialRecordRepository);
            PayRollService payRollService = new PayRollService(payrollRepository);
            TaxService taxService = new TaxService(taxRepository);

            // Initialize the PayrollSystem class with the services
            PayrollSystem payrollSystem = new PayrollSystem(employeeService, financialRecordService, payRollService, taxService);
            payrollSystem.Run();
            

        }
    }
}
