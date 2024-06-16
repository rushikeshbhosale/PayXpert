

using Moq;
using NUnit.Framework;
using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Repository;

namespace PayXpertTests
{
    [TestFixture]
    public class PayXpertTests
    {
        [Test]
        public void CalculateGrossSalaryForEmployee_Test()
        {
            // Arrange
            var employeeId = 1;
            var mockedEmployeeRepository = new Mock<EmployeeRepository>();
            mockedEmployeeRepository.Setup(repo => repo.GetEmployeeById(employeeId)).Returns(new Employee
            {
               EmployeeID = 1
               Basic
            });
            var employeeService = new EmployeeService(mockedEmployeeRepository.Object);

            // Act
            var grossSalary = employeeService.CalculateGrossSalaryForEmployee(employeeId);

            // Assert
            // You should assert that the grossSalary matches the expected value based on the employee's salary and any other factors
            Assert.AreEqual(50000m, grossSalary);
        }
    }
}
