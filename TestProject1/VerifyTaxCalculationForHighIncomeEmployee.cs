using PayXpert_Database_Connectivity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpertTests
{
    internal class VerifyTaxCalculationForHighIncomeEmployee
    {
        private TaxRepository taxRepository;
        [SetUp]
        public void Setup()
        {
            taxRepository = new TaxRepository();
        }
        [Test]
        public void VerifyHighTaxEmployee()
        {
            decimal highGrossSalary = 10000.00m; 
            decimal expectedTaxAmount = 2000.00m; 

            // Act
            decimal actualTaxAmount = taxRepository.CalculateTax(highGrossSalary);

            //Assert
            Assert.That(expectedTaxAmount, Is.EqualTo(actualTaxAmount),"HighIncome employee Tax calculation Fails");

        }
    }
}
