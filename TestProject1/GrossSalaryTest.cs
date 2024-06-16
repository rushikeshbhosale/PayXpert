using PayXpert_Database_Connectivity.Repository;

namespace PayXpertTests
{
    public class GrossSalaryTest
    {
        PayrollRepository payrollRepository;
        [SetUp]
        public void Setup()
        {
            payrollRepository = new PayrollRepository();
        }

        [Test]
        public void GrossSalaryTestCheck()
        {
            //Arrange
            decimal basicSalary = 12000.00m;
            decimal overtimePay = 1200.00m;
            decimal expectedGrossSalary = basicSalary + overtimePay;

            //Act
            decimal actualGrossSalary = payrollRepository.GetGrossSalary(basicSalary, overtimePay);

            //Assert
            Assert.That(actualGrossSalary, Is.EqualTo(expectedGrossSalary), "Gross salary calculation is incorrect.");
        }
    }
}