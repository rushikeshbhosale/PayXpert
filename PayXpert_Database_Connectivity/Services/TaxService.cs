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
    public class TaxService
    {
       private readonly ITaxService _taxRepository;

        
        public TaxService(ITaxService taxRepositoy)
        {
            _taxRepository = taxRepositoy;
        }

        //Tax Menu method-1, CalculateTax
        public void CalculateTax()
        {
            try
            {
                Console.WriteLine("Enter the Employee ID: ");
                int empId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the tax Year:");
                int taxYear = Convert.ToInt32(Console.ReadLine());

                _taxRepository.CalculateTax(empId, taxYear);
            }
            catch(TaxCalculationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            { 
                Console.WriteLine(ex.Message);   
            }
        }

        //Tax Menu method-2, GetTaxById
        public void GetTaxById()
        {
            try
            {
                Console.WriteLine("Enter the Tax ID: ");
                int taxId = Convert.ToInt32(Console.ReadLine());

                Tax tax = _taxRepository.GetTaxById(taxId);
                if (tax != null)
                {
                    Console.WriteLine($"Tax ID: {tax.TaxID} | Employee ID: {tax.EmployeeID} | Tax Year: {tax.TaxYear} | Tax Amount: {tax.TaxAmount} | Tax Date: {tax.TaxAmount}");
                }
                else
                {
                    Console.WriteLine("Tax record not found.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Tax Menu method-3, GetTaxesForEmployee
        public void GetTaxesForEmployee()
        {
            try
            {
                Console.WriteLine("Enter the Employee ID: ");
                int empId = Convert.ToInt32(Console.ReadLine());

               _taxRepository.GetTaxesForEmployee(empId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Tax Menu method-4, GetTaxesForYear
        public void GetTaxesForYear()
        {
            Console.WriteLine("Enter the Year: ");
            int year = Convert.ToInt32(Console.ReadLine());

            _taxRepository.GetTaxesForYear(year);   
        }

    }
}
