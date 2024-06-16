using PayXpert_Database_Connectivity.Models;
using System.Text.RegularExpressions;



namespace PayXpert_Database_Connectivity.Repository
{
    public class ValidationRepository
    {
        public bool ValidateEmployee(Employee employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Employee object is null.");
                return false;
            }

            // Check if required fields are not null or empty
            if (string.IsNullOrEmpty(employee.FirstName))
            {
                Console.WriteLine("First name is required.");
                return false;
            }

            if (string.IsNullOrEmpty(employee.LastName))
            {
                Console.WriteLine("Last name is required.");
                return false;
            }

            if (employee.DateOfBirth == null)
            {
                Console.WriteLine("Date of birth is required.");
                return false;
            }

            // Validate email format
            if (!IsValidEmail(employee.Email))
            {
                
                return false;
            }

            // Validate phone number format
            if (!IsValidPhoneNumber(employee.PhoneNumber))
            {
                Console.WriteLine("Invalid phone number format.");
                return false;
            }

           //If all pass, then true
            return true;
        }
        public bool ValidateSalary(decimal salary)
        {
            // salary is non-negative
            if (salary < 0)
            {
                Console.WriteLine("Salary amount must be non-negative.");
                return false;
            }
            return true;
        }
        private bool IsValidEmail(string email)
        {
            //Standard Pattern for email
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            //match with mattern
            Match match = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

        //Helper method to validate phone number format
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length >= 10;
        }

        public bool validSalary(decimal salary)
        {
            return salary > 0;
        }
    }
}
