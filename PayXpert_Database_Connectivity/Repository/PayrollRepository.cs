using PayXpert_Database_Connectivity.Exceptions;
using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Repository
{
    public class PayrollRepository : IPayrollService
    {
        //Add Payroll to DB
        void IPayrollService.GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate, decimal basicSalary, decimal deductions, decimal overtimePay)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                // Calculate net salary
                decimal netSalary = basicSalary + overtimePay - deductions;

                string insertQuery = @"
                                       INSERT INTO Payroll (employee_id, payPeriodStartDate, payPeriodEndDate, basic_salary, ot_pay, deductions, net_salary)
                                       VALUES (@EmployeeId, @StartDate, @EndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary);
                                       SELECT SCOPE_IDENTITY();
";

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    // Parameters
                    insertCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    insertCommand.Parameters.AddWithValue("@StartDate", startDate);
                    insertCommand.Parameters.AddWithValue("@EndDate", endDate);
                    insertCommand.Parameters.AddWithValue("@BasicSalary", basicSalary);
                    insertCommand.Parameters.AddWithValue("@OvertimePay", overtimePay);
                    insertCommand.Parameters.AddWithValue("@Deductions", deductions);
                    insertCommand.Parameters.AddWithValue("@NetSalary", netSalary);

                    connection.Open();
                    
                    int insertedPayrollId = Convert.ToInt32(insertCommand.ExecuteScalar());

                    if (insertedPayrollId > 0)
                    {
                        Console.WriteLine("Payroll generated and added to the database successfully. Payroll ID: " + insertedPayrollId+" with NetSalary"+netSalary);
                    }
                    else
                    {
                        throw new PayrollGenerationException("Error: Generating payroll for employee.");
                    }
                }
            }
        }

        PayRoll IPayrollService.GetPayrollById(int payrollId)
        {
            PayRoll payRoll = null;
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Payroll WHERE payroll_id = @PayrollId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PayrollId", payrollId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        payRoll = new PayRoll
                        {
                            PayrollID = reader.GetInt32(reader.GetOrdinal("payroll_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            PayPeriodStartDate = reader.GetDateTime(reader.GetOrdinal("payPeriodStartDate")),
                            PayPeriodEndDate = reader.GetDateTime(reader.GetOrdinal("payPeriodEndDate")),
                            BasicSalary = reader.GetDecimal(reader.GetOrdinal("basic_salary")),
                            OvertimePay = reader.GetDecimal(reader.GetOrdinal("ot_pay")),
                            Deductions = reader.GetDecimal(reader.GetOrdinal("deductions")),
                            NetSalary = reader.GetDecimal(reader.GetOrdinal("net_salary"))
                        };
                    }
                }
            }
            return payRoll;
        }
  

        List<PayRoll> IPayrollService.GetPayrollsForEmployee(int employeeId)
        {
            List<PayRoll> payRollList = new List<PayRoll> { };

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Payroll WHERE employee_id = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Add data from DB to Payroll Object
                        PayRoll payroll = new PayRoll
                        {
                            PayrollID = reader.GetInt32(reader.GetOrdinal("payroll_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            PayPeriodStartDate = reader.GetDateTime(reader.GetOrdinal("payPeriodStartDate")),
                            PayPeriodEndDate = reader.GetDateTime(reader.GetOrdinal("payPeriodEndDate")),
                            BasicSalary = reader.GetDecimal(reader.GetOrdinal("basic_salary")),
                            OvertimePay = reader.GetDecimal(reader.GetOrdinal("ot_pay")),
                            Deductions = reader.GetDecimal(reader.GetOrdinal("deductions")),
                            NetSalary = reader.GetDecimal(reader.GetOrdinal("net_salary"))
                        };

                        payRollList.Add(payroll);
                    }
                }
            }
            PrintPayroll(payRollList);
            return payRollList;
        }

        List<PayRoll> IPayrollService.GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<PayRoll> payrollList = new List<PayRoll>();
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Payroll WHERE payPeriodStartDate >= @StartDate AND payPeriodEndDate <= @EndDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        PayRoll payroll = new PayRoll
                        {
                            PayrollID = reader.GetInt32(reader.GetOrdinal("payroll_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            PayPeriodStartDate = reader.GetDateTime(reader.GetOrdinal("payPeriodStartDate")),
                            PayPeriodEndDate = reader.GetDateTime(reader.GetOrdinal("payPeriodEndDate")),
                            BasicSalary = reader.GetDecimal(reader.GetOrdinal("basic_salary")),
                            OvertimePay = reader.GetDecimal(reader.GetOrdinal("ot_pay")),
                            Deductions = reader.GetDecimal(reader.GetOrdinal("deductions")),
                            NetSalary = reader.GetDecimal(reader.GetOrdinal("net_salary"))
                        };

                        payrollList.Add(payroll);
                    }
                }
            }
            PrintPayroll(payrollList);
            return payrollList;
        }

        public void PrintPayroll(List<PayRoll> payrollList)
        {
            if (payrollList == null || payrollList.Count == 0)
            {
                throw new PayrollGenerationException("Error generating payroll for employee.");
            }

            Console.WriteLine("Payroll Details:");
            foreach (var payroll in payrollList)
            {
                Console.WriteLine($"Payroll ID: {payroll.PayrollID} | Employee ID: {payroll.EmployeeID} | Pay Period Start Date: {payroll.PayPeriodStartDate.ToShortDateString()} | Pay Period End Date: {payroll.PayPeriodEndDate.ToShortDateString()} | Basic Salary: {payroll.BasicSalary} | Overtime Pay: {payroll.OvertimePay} | Deductions: {payroll.Deductions} | Net Salary: {payroll.NetSalary}");
            }

        }

        public decimal GetGrossSalary(decimal basicSalary, decimal overtimePay)
        {
            return basicSalary + overtimePay;
        }

        public decimal calcluateNetSalary(decimal grossSalary, decimal deductions)
        {
            return grossSalary - deductions;
        }
    }
}
