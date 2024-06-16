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
    public class FinancialRecordRepository : IFinancialRecordService
    {
        int IFinancialRecordService.AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "INSERT INTO FinancialRecord (employee_id, descriptions, amount, record_type, record_date) " +
                               "VALUES (@EmployeeId, @Description, @Amount, @RecordType, GETDATE());SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@RecordType", recordType);

                    connection.Open();
                    int insertedId = Convert.ToInt32(command.ExecuteScalar());

                    Console.WriteLine(insertedId > 0 ? $"Financial record added successfully with ID: {insertedId}." : throw new FinancialRecordException("Error managing financial records."));
                    return insertedId;
                }
            }
        }

        FinancialRecord IFinancialRecordService.GetFinancialRecordById(int recordId)
        {
            FinancialRecord record = null;

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM FinancialRecord WHERE record_id = @RecordId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecordId", recordId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        record = new FinancialRecord
                        {
                            RecordID = reader.GetInt32(reader.GetOrdinal("record_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            RecordDate = reader.GetDateTime(reader.GetOrdinal("record_date")),
                            Description = reader.GetString(reader.GetOrdinal("descriptions")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                            RecordType = reader.GetString(reader.GetOrdinal("record_type"))
                        };
                    }
                    return record;
                }
            }

           

        }

        public void PrintRecordDetails(List<FinancialRecord> records)
        {
            if (records == null || records.Count == 0)
            {
                throw new FinancialRecordException("Error managing financial records.");
            }

            foreach (var record in records)
            {
                Console.WriteLine($"Record ID: {record.RecordID} | Employee ID: {record.EmployeeID} | Record Date: {record.RecordDate.ToShortDateString()} | Description: {record.Description} | Amount: {record.Amount} | Record Type: {record.RecordType}");
                Console.WriteLine();
            }
        }

        List<FinancialRecord> IFinancialRecordService.GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM FinancialRecord WHERE record_date = @RecordDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecordDate", recordDate);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord
                        {
                            RecordID = reader.GetInt32(reader.GetOrdinal("record_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            RecordDate = reader.GetDateTime(reader.GetOrdinal("record_date")),
                            Description = reader.GetString(reader.GetOrdinal("descriptions")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                            RecordType = reader.GetString(reader.GetOrdinal("record_type"))
                        };
                        records.Add(record);
                    }
                }
            }
            PrintRecordDetails(records);
            return records;
        }

        List<FinancialRecord> IFinancialRecordService.GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();

            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM FinancialRecord WHERE employee_id = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord
                        {
                            RecordID = reader.GetInt32(reader.GetOrdinal("record_id")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            RecordDate = reader.GetDateTime(reader.GetOrdinal("record_date")),
                            Description = reader.GetString(reader.GetOrdinal("descriptions")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                            RecordType = reader.GetString(reader.GetOrdinal("record_type"))
                        };
                        records.Add(record);
                    }
                }
            }
            return records;
        }
    }
}
