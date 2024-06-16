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
    public class FinancialRecordService
    {
        private readonly IFinancialRecordService _financialRecordService;

        // Constructor to inject the Finanicial record repo
        public FinancialRecordService(IFinancialRecordService financialRecordService)
        {
            _financialRecordService = financialRecordService;
        }

        //Finacial record menu method-1 AddFinancialRecord
        public void AddFinancialRecord()
        {
            try
            {
                Console.WriteLine("Enter Financial Record Details:");

                Console.Write("Employee ID: ");
                int employeeId = int.Parse(Console.ReadLine());

                Console.Write("Description: ");
                string description = Console.ReadLine();

                Console.Write("Amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                Console.Write("Record Type: ");
                string recordType = Console.ReadLine();
     
                _financialRecordService.AddFinancialRecord(employeeId, description, amount, recordType);
               
            }
            catch(FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);        
            }
          
        }

        //Finacial record menu method-2 GetFinancialRecordById
        public void GetFinancialRecordById()
        {
            try
            {
                Console.WriteLine("Enter the Record Id to retreive:");
                int recordId = Convert.ToInt32(Console.ReadLine());

                FinancialRecord record = _financialRecordService.GetFinancialRecordById(recordId);
                if (record != null)//To print 
                {
                    Console.WriteLine($"Record ID: {record.RecordID} | Employee ID: {record.EmployeeID} | Record Date: {record.RecordDate.ToShortDateString()} | Description: {record.Description} | Amount: {record.Amount} | Record Type: {record.RecordType}");
                }
                else
                {
                    throw new FinancialRecordException("Financial record not found.");
                }
            }
            catch (FinancialRecordException ex) 
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        //Finacial record menu method-3 GetFinancialRecordsForEmployee
        public void GetFinancialRecordsForEmployee()
        {

            try
            {
                Console.WriteLine("Enter the employee Id to retreive:");
                int recordId = Convert.ToInt32(Console.ReadLine());

                List<FinancialRecord> records = _financialRecordService.GetFinancialRecordsForEmployee(recordId);
                var financialRecordRepository = new FinancialRecordRepository();
                financialRecordRepository.PrintRecordDetails(records);//Print to comnsole

            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Finacial record menu method-4 GetFinancialRecordsForDate
        public void GetFinancialRecordsForDate()
        {
            try
            {
                Console.Write("Enter the record date (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime recordDate))
                {
                    _financialRecordService.GetFinancialRecordsForDate(recordDate);
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
}
