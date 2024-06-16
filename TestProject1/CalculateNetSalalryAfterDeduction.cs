using NUnit.Compatibility;
using PayXpert_Database_Connectivity.Repository;

namespace PayXpertTests
{
    internal class CalculateNetSalalryAfterDeduction
    {
        private PayrollRepository payrollRepository;
        [SetUp]
        public void Setup()
        {
            payrollRepository = new PayrollRepository();
        }

        [Test]
        public void CalculateNetSalary_ReturnsCorrect()
        {
            //Assign
            decimal grossSalary = 7000.00m;
            decimal deductions = 1000.00m;
            decimal expectedNetSalary = 6000.00m;

            //Act
            decimal actualNetSalary = payrollRepository.calcluateNetSalary(grossSalary, deductions);

            //Assert
            Assert.That(actualNetSalary, Is.EqualTo(expectedNetSalary), "Net salary calculation is incorrect.");
        }

    }
}
