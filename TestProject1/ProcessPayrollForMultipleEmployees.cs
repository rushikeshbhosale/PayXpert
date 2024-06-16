using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpertTests
{
    internal class ProcessPayrollForMultipleEmployees
    {
        IPayrollService payrollService;
        [SetUp]
        public void Setup()
        {
            payrollService = new PayrollRepository();
        }
        [Test]
        public void ProcessPayrollForMultipleEmployees_Test()
        {
            //Arrange
            int employee1Id = 4;
            DateTime startDate1 = new DateTime(2024, 02, 1);
            DateTime endDate1 = new DateTime(2024, 03, 02);

            int employee2Id = 7;
            DateTime startDate2 = new DateTime(2024, 03, 1);
            DateTime endDate2 = new DateTime(2024, 04, 15);
            decimal basicSalary = 3000;
            decimal deductions = 550;
            decimal overtimePay = 600;

            //Act;
            payrollService.GeneratePayroll(employee1Id, startDate1, endDate1, basicSalary, deductions, overtimePay);
            payrollService.GeneratePayroll(employee2Id, startDate2, endDate2, basicSalary, deductions, overtimePay);

            //Assert
            Assert.DoesNotThrow(() => payrollService.GeneratePayroll(employee1Id, startDate1, endDate1, basicSalary, deductions, overtimePay));

            Assert.DoesNotThrow(() => payrollService.GeneratePayroll(employee2Id, startDate2, endDate2, basicSalary, deductions, overtimePay));




        }
    }
}
