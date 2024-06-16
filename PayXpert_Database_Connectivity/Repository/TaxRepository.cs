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
    public class TaxRepository : ITaxService
    {
        void ITaxService.CalculateTax(int employeeId, int taxYear)
        {
            // Retrieve taxable income from DB
            decimal taxableIncome = GetTaxableIncome(employeeId);

            // Calculate tax amount
            decimal taxAmount = (taxableIncome > 10000.0m) ? taxableIncome * 0.15m : taxableIncome * 0.10m;

            // Insert tax details into the Tax table
            InsertTaxDetails(employeeId, taxYear,taxableIncome, taxAmount);

            


        }

        private void InsertTaxDetails(int employeeId, int taxYear,decimal taxableIncome, decimal taxAmount)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "INSERT INTO Tax (employee_id, tax_year,taxable_income, tax_amount) VALUES (@EmployeeId, @TaxYear,@TaxableIncome, @TaxAmount)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@TaxYear", taxYear);
                    command.Parameters.AddWithValue("@TaxableIncome", taxableIncome); 
                    command.Parameters.AddWithValue("@TaxAmount", taxAmount);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected>0)
                    {
                        Console.WriteLine($"The Tax Amount for Employee ID: {employeeId} for the year {taxYear} = {taxAmount}");
                    }
                }
            }
        }

        private decimal GetTaxableIncome(int employeeId)
        {
            decimal taxableIncome = -1;
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT net_salary FROM Payroll WHERE employee_id = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        taxableIncome = Convert.ToDecimal(result);
                    }
                    else
                    {
                        throw new TaxCalculationException("Payroll record for the employee not found.");
                    }
                }
            }
            return taxableIncome;
        }

        Tax ITaxService.GetTaxById(int taxId)
        {
            Tax tax = null;
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Tax WHERE tax_id = @TaxId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaxId", taxId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tax = new Tax
                        {
                            TaxID = reader.GetInt32(reader.GetOrdinal("tax_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            TaxYear = reader.GetInt32(reader.GetOrdinal("tax_year")),
                            TaxableIncome = reader.GetDecimal(reader.GetOrdinal("taxable_income")),
                            TaxAmount = reader.GetDecimal(reader.GetOrdinal("tax_amount"))
                        };
                    }
                }
            }
            return tax;
        }

        List<Tax> ITaxService.GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxes = new List<Tax>();
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Tax WHERE employee_id = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Tax tax = new Tax
                        {
                            TaxID = reader.GetInt32(reader.GetOrdinal("tax_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            TaxYear = reader.GetInt32(reader.GetOrdinal("tax_year")),
                            TaxableIncome = reader.GetDecimal(reader.GetOrdinal("taxable_income")),
                            TaxAmount = reader.GetDecimal(reader.GetOrdinal("tax_amount"))
                        };
                        taxes.Add(tax);
                    }
                }
            }
            PrintTaxList(taxes);
            return taxes;
        }

        List<Tax> ITaxService.GetTaxesForYear(int taxYear)
        {
            List<Tax> taxes = new List<Tax>();
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Tax WHERE tax_year = @TaxYear";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaxYear", taxYear);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Tax tax = new Tax
                        {
                            TaxID = reader.GetInt32(reader.GetOrdinal("tax_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            TaxYear = reader.GetInt32(reader.GetOrdinal("tax_year")),
                            TaxableIncome = reader.GetDecimal(reader.GetOrdinal("taxable_income")),
                            TaxAmount = reader.GetDecimal(reader.GetOrdinal("tax_amount"))
                        };
                        taxes.Add(tax);
                    }
                }
            }
            PrintTaxList(taxes);
            return taxes;
        }

        public void PrintTaxList(List<Tax> taxes)
        {
            if (taxes == null || taxes.Count == 0)
            {
                Console.WriteLine("No tax records found.");
                return;
            }

            Console.WriteLine("Tax Details:");
            foreach (var tax in taxes)
            {
                Console.WriteLine($"Tax ID: {tax.TaxID} | Employee ID: {tax.EmployeeID} | Tax Year: {tax.TaxYear} | Taxable Income: {tax.TaxableIncome} | Tax Amount: {tax.TaxAmount}");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
            }
        }

        public decimal CalculateTax(decimal grossSalary)
        {
            decimal taxRate = grossSalary > 5000 ? 0.20m : 0.15m;
            decimal taxAmount = grossSalary * taxRate;
            return taxAmount;
        }
    }
}
 
