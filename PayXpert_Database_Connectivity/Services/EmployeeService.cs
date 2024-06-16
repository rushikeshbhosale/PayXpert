using PayXpert_Database_Connectivity.Exceptions;
using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Repository;


namespace PayXpert_Database_Connectivity.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeService _employeeRepository;

        // Constructor to inject the EmployeeRepository
        public EmployeeService(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        //Employee Managemnt Menu Method-1, GetEmployeeById
        public void GetEmployeeById()
        {
            try
            {
                //get User input
                Console.WriteLine("Enter Employee ID: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                //Store it in employee object
                var employee = _employeeRepository.GetEmployeeById(employeeId);

                //print to console
                Console.WriteLine();
                EmployeeRepository empRepo = new EmployeeRepository();
                empRepo.PrintEmployeeDetail(employee);
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(DatabaseConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Employee Managemnt Menu Method-2, GetAllEmployee and print
        public void  GetAllEmployees()
        {
            _employeeRepository.GetAllEmployees();            
        }

        //Employee Managemnt Menu Method-3, AddEmployee
        public int AddEmployee()
        {
            try
            {
                ValidationRepository validateEmployee = new ValidationRepository();
                Console.WriteLine("Enter Employee Details:");

                Console.Write("First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Date of Birth (YYYY-MM-DD): ");
                DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Gender: ");
                string gender = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Console.Write("Address: ");
                string address = Console.ReadLine();

                Console.Write("Position: ");
                string position = Console.ReadLine();

                Console.Write("Joining Date (YYYY-MM-DD): ");
                DateTime joiningDate = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Termination Date (optional, leave empty if not applicable, YYYY-MM-DD): ");
                string terminationDateInput = Console.ReadLine();
                DateTime? terminationDate = string.IsNullOrWhiteSpace(terminationDateInput) ? null : DateTime.Parse(terminationDateInput);
                // Create an Employee object with the provided input
                Employee newEmployee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Gender = gender,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Position = position,
                    JoiningDate = joiningDate,
                    TerminationDate = terminationDate
                };
                bool validation = validateEmployee.ValidateEmployee(newEmployee);
                if (validation)
                {
                   int newEmpId= _employeeRepository.AddEmployee(newEmployee);
                    return newEmpId;
                    
                }
                else
                {
                    throw new InvalidInputException("Error: Enter the valid input!");
                }
                
            }
            catch(DatabaseConnectionException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            catch(InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        //Employee Managemnt Menu Method-4, Update Employee
        public void UpdateEmployee()
        {
            try
            {
                ValidationRepository validateEmployee = new ValidationRepository();
                Console.WriteLine("Enter Employee Details:");

                Console.WriteLine("Enter the employee Id you want to update:");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                Console.Write("First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Date of Birth (YYYY-MM-DD): ");
                DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Gender: ");
                string gender = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Console.Write("Address: ");
                string address = Console.ReadLine();

                Console.Write("Position: ");
                string position = Console.ReadLine();

                Console.Write("Joining Date (YYYY-MM-DD): ");
                DateTime joiningDate = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Termination Date (optional, leave empty if not applicable, YYYY-MM-DD): ");
                string terminationDateInput = Console.ReadLine();
                DateTime? terminationDate = string.IsNullOrWhiteSpace(terminationDateInput) ? null : DateTime.Parse(terminationDateInput);
               
                // Create an Employee object with the provided input
                Employee updatedEmployee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Gender = gender,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Position = position,
                    JoiningDate = joiningDate,
                    TerminationDate = terminationDate
                };
                bool validation = validateEmployee.ValidateEmployee(updatedEmployee);
                if (validation)
                {
                    _employeeRepository.UpdateEmployee(employeeId, updatedEmployee);
                    Console.WriteLine("Employee Data updated!");
                }
                else
                {
                    throw new InvalidInputException("Error: Enter the valid input!");
                }
            }
            catch(EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //Employee Managemnt Menu Method-5, Remove Employee
        public void RemoveEmployee()
        {
            try
            {
                Console.WriteLine("Enter the Employee Id to Remove:");
                int employeeId = Convert.ToInt32(Console.ReadLine());
                _employeeRepository.RemoveEmployee(employeeId);
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Employee Managemnt Menu Method-6, Remove Employee


    }
}
