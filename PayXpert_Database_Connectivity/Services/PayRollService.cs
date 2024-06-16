using PayXpert_Database_Connectivity.Exceptions;
using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Services
{
    public class PayRollService
    {
        private readonly IPayrollService _payrollService;

        // Constructor to inject the PayRoll repo
        public PayRollService(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        //PayRoll Processing Menu Method-1, Generate Payroll
        public void GeneratePayroll()
        {
            try
            {
                Console.Write("Enter employee ID: ");
                int employeeId;
                while (!int.TryParse(Console.ReadLine(), out employeeId) || employeeId <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid employee ID.");
                    Console.Write("Enter employee ID: ");
                }

                //input for start date
                Console.Write("Enter start date (YYYY-MM-DD): ");
                DateTime startDate;
                while (!DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    Console.WriteLine("Invalid input. Please enter a valid start date (YYYY-MM-DD).");
                    Console.Write("Enter start date (YYYY-MM-DD): ");
                }

                // Get user input for end date
                Console.Write("Enter end date (YYYY-MM-DD): ");
                DateTime endDate;
                while (!DateTime.TryParse(Console.ReadLine(), out endDate) || endDate < startDate)
                {
                    Console.WriteLine("Invalid input. Please enter a valid end date (YYYY-MM-DD) that is after the start date.");
                    Console.Write("Enter end date (YYYY-MM-DD): ");
                }

                // Get user input for basic salary
                Console.Write("Enter basic salary: ");
                decimal basicSalary;
                while (!decimal.TryParse(Console.ReadLine(), out basicSalary) || basicSalary <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid basic salary.");
                    Console.Write("Enter basic salary: ");
                }

                // for deductions
                Console.Write("Enter deductions: ");
                decimal deductions;
                while (!decimal.TryParse(Console.ReadLine(), out deductions) || deductions < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid deduction amount.");
                    Console.Write("Enter deductions: ");
                }

                //input for overtime
                Console.Write("Enter overtime pay: ");
                decimal overtimePay;
                while (!decimal.TryParse(Console.ReadLine(), out overtimePay) || overtimePay < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid overtime pay amount.");
                    Console.Write("Enter overtime pay: ");
                }
                _payrollService.GeneratePayroll(employeeId, startDate, endDate, basicSalary, deductions, overtimePay);
            }
            catch(PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //PayRoll Processing Menu Method-2, Get Payroll by ID
        public void GetPayrollById()
        {
            try
            {
                Console.WriteLine("Enter the PayRoll Id:");
                int payrollId = Convert.ToInt32(Console.ReadLine());
                PayRoll payroll = _payrollService.GetPayrollById(payrollId);
                if (payroll != null)
                {
                    Console.WriteLine($"Payroll ID: {payroll.PayrollID} | Employee ID: {payroll.EmployeeID} | Start Date: {payroll.PayPeriodStartDate.ToShortTimeString()} | End Date: {payroll.PayPeriodStartDate.ToShortTimeString()} | Basic Salary: {payroll.BasicSalary} | Deductions: {payroll.Deductions} | Overtime Pay: {payroll.OvertimePay} | Net Salary: {payroll.NetSalary}");
                }
                else
                {
                    throw new PayrollGenerationException("Error: Payroll Not Found");
                }
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        //PayRoll Processing Menu Method-3,Get Payrolls for Employee
        public void GetPayrollsForEmployee()
        {
            try
            {
                Console.WriteLine("Enter the Employee Id:");
                int empId = Convert.ToInt32(Console.ReadLine());
                List<PayRoll> payrolls = _payrollService.GetPayrollsForEmployee(empId);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        //PayRoll Processing Menu Method-4,Get Payrolls for Period
        public void GetPayrollsForPeriod()
        {
            try
            {
                Console.WriteLine("Enter Start Date (YYYY-MM-DD): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter End Date (YYYY-MM-DD): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                _payrollService.GetPayrollsForPeriod(startDate, endDate);
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }






    }
}
