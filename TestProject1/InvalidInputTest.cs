using PayXpert_Database_Connectivity.Exceptions;
using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpertTests
{
    internal class InvalidInputTest
    {

        public ValidationRepository validation;
        [SetUp]
        public void Setup()
        {
           validation = new ValidationRepository();
        }

        [Test]
        public void InvalidInput_test()
        {
            //Arrange - ready an mock invalid employee object
            string FirstName = "";//Invalid
            string LastName = null;//Invalid
            DateTime DateOfBirth = new DateTime(2002, 07, 03);
            string  Gender = "Male";
            string Email =  "Sample.com";//Invalid
            string PhoneNumber = "65354585";//Invalid
            string Address = "Mainstreet";
            string Position = "Manger";
            DateTime JoiningDate = new DateTime(2022, 05, 03); 
            DateTime TerminationDate = new DateTime(2025, 05, 03);

            //Act - initilaize employee object
            Employee invalidEmployee = new Employee()
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                Gender = Gender,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Address = Address,
                Position = Position,
                JoiningDate = JoiningDate,
                TerminationDate = TerminationDate
            };

            //Assert
            Assert.That(false, Is.EqualTo(validation.ValidateEmployee(invalidEmployee)));
        }
    }
}
