using PayXpert_Database_Connectivity.Models;

namespace PayXpert_Database_Connectivity.Repository
{
    public class ReportGeneratorRepository
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPayrollService _payrollService;
        private readonly ITaxService _taxService;
        private readonly IFinancialRecordService _financialRecordService;

        public ReportGeneratorRepository(IEmployeeService employeeService, IPayrollService payrollService, ITaxService taxService, IFinancialRecordService financialRecordService)
        {
            _employeeService = employeeService;
            _payrollService = payrollService;
            _taxService = taxService;
            _financialRecordService = financialRecordService;
        }

        //Method to generate a payroll report for a employee
        public void GeneratePayrollReport(int employeeId)
        {
            Employee employee = _employeeService.GetEmployeeById(employeeId);
            if (employee != null)
            {
                List<PayRoll> payrolls = _payrollService.GetPayrollsForEmployee(employeeId);

                Console.WriteLine($"Payroll Report for Employee ID: {employee.EmployeeID}");
                Console.WriteLine($"Employee Name: {employee.FirstName} {employee.LastName}");
                Console.WriteLine("-------------------------------------------------");

                foreach (var payroll in payrolls)
                {
                    Console.WriteLine($"Payroll ID: {payroll.PayrollID}");
                    Console.WriteLine($"Pay Period: {payroll.PayPeriodStartDate.ToShortDateString()} - {payroll.PayPeriodEndDate.ToShortDateString()}");
                    Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                    Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                    Console.WriteLine($"Deductions: {payroll.Deductions}");
                    Console.WriteLine($"Net Salary: {payroll.NetSalary}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
        }

        //Method to generate a tax report for a employee
        public void GenerateTaxReport(int employeeId)
        {
            Employee employee = _employeeService.GetEmployeeById(employeeId);
            if (employee != null)
            {
                List<Tax> taxes = _taxService.GetTaxesForEmployee(employeeId);

                Console.WriteLine($"Tax Report for Employee ID: {employee.EmployeeID}");
                Console.WriteLine($"Employee Name: {employee.FirstName} {employee.LastName}");
                Console.WriteLine("-------------------------------------------------");

                foreach (var tax in taxes)
                {
                    Console.WriteLine($"Tax ID: {tax.TaxID}");
                    Console.WriteLine($"Tax Year: {tax.TaxYear}");
                    Console.WriteLine($"Taxable Income: {tax.TaxableIncome}");
                    Console.WriteLine($"Tax Amount: {tax.TaxAmount}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
        }

        //Method to generate a financial record report for a employee
        public void GenerateFinancialRecordReport(int employeeId)
        {
            Employee employee = _employeeService.GetEmployeeById(employeeId);
            if (employee != null)
            {
                List<FinancialRecord> financialRecords = _financialRecordService.GetFinancialRecordsForEmployee(employeeId);

                Console.WriteLine($"Financial Record Report for Employee ID: {employee.EmployeeID}");
                Console.WriteLine($"Employee Name: {employee.FirstName} {employee.LastName}");
                Console.WriteLine("-------------------------------------------------");

                foreach (var record in financialRecords)
                {
                    Console.WriteLine($"Record ID: {record.RecordID}");
                    Console.WriteLine($"Record Date: {record.RecordDate.ToShortDateString()}");
                    Console.WriteLine($"Description: {record.Description}");
                    Console.WriteLine($"Amount: {record.Amount}");
                    Console.WriteLine($"Record Type: {record.RecordType}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
        }
    }
}
