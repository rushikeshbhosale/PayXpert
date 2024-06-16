using PayXpert_Database_Connectivity.Exceptions;
using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Repository
{
    public class EmployeeRepository : IEmployeeService
    {
        //Add employee details to DB
        int IEmployeeService.AddEmployee(Employee employeeData)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"
                                INSERT INTO Employees (first_name, last_name, dob, gender, email, phone_no, address, position, join_date, Termin_Date)
                                VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate);
                                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                //Add parameters
                command.Parameters.AddWithValue("@FirstName", employeeData.FirstName);
                command.Parameters.AddWithValue("@LastName", employeeData.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", employeeData.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", employeeData.Gender);
                command.Parameters.AddWithValue("@Email", employeeData.Email);
                command.Parameters.AddWithValue("@PhoneNumber", employeeData.PhoneNumber);
                command.Parameters.AddWithValue("@Address", employeeData.Address);
                command.Parameters.AddWithValue("@Position", employeeData.Position);
                command.Parameters.AddWithValue("@JoiningDate", employeeData.JoiningDate);
                command.Parameters.AddWithValue("@TerminationDate", employeeData.TerminationDate.HasValue ? (object)employeeData.TerminationDate.Value : DBNull.Value); //handle null
               

                connection.Open();
               
                int insertedEmployeeId = Convert.ToInt32(command.ExecuteScalar());

                Console.WriteLine((insertedEmployeeId > 0)?"Employee Added Sucessfully":throw new DatabaseConnectionException("Error: database Connection"));
                return insertedEmployeeId;
            }
            
        }

        List<Employee> IEmployeeService.GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Employees";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("dob")),
                            Gender = reader.GetString(reader.GetOrdinal("gender")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("phone_no")),
                            Address = reader.GetString(reader.GetOrdinal("address")),
                            Position = reader.GetString(reader.GetOrdinal("position")),
                            JoiningDate = reader.GetDateTime(reader.GetOrdinal("join_date")),
                            TerminationDate = reader.IsDBNull(reader.GetOrdinal("Termin_Date")) ? null : reader.GetDateTime(reader.GetOrdinal("Termin_Date"))
                        };

                        employees.Add(employee);
                    }
                }   
            } 
            PrintEmployeeDetails(employees);
            return employees;
        }

        Employee IEmployeeService.GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "SELECT * FROM Employees WHERE employee_id = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();//execution starts

                    if (reader.Read())
                    {
                       employee = new Employee
                        {
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("employee_id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("dob")),
                            Gender = reader.GetString(reader.GetOrdinal("gender")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("phone_no")),
                            Address = reader.GetString(reader.GetOrdinal("address")),
                            Position = reader.GetString(reader.GetOrdinal("position")),
                            JoiningDate = reader.GetDateTime(reader.GetOrdinal("join_date")),
                            TerminationDate = reader.IsDBNull(reader.GetOrdinal("Termin_Date")) ? null : reader.GetDateTime(reader.GetOrdinal("Termin_Date"))
                        };
                    }
                }
            }
            return employee;
        }
        //To print the details in list to console
        public void PrintEmployeeDetails(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"| Employee ID: {employee.EmployeeID} | " +
                                  $"Name: {employee.FirstName} {employee.LastName} | " +
                                  $"Date of Birth: {employee.DateOfBirth.ToShortDateString()} | " +
                                  $"Gender: {employee.Gender} | " +
                                  $"Email: {employee.Email} | " +
                                  $"Phone Number: {employee.PhoneNumber} | " +
                                  $"Address: {employee.Address} | \n" +
                                  $"Position: {employee.Position} | " +
                                  $"Joining Date: {employee.JoiningDate.ToShortDateString()} | " +
                                  $"Termination Date: {(employee.TerminationDate != null ? employee.TerminationDate.Value.ToShortDateString() : "Not terminated")} |");
                Console.WriteLine();
            }
        }

        void IEmployeeService.RemoveEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = "DELETE FROM Employees WHERE employee_id = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Employee with ID {employeeId} removed successfully.");
                    }
                    else
                    {
                        throw new EmployeeNotFoundException("Employee Not found in the database");
                    }
                }
            }
        }

        void IEmployeeService.UpdateEmployee(int employeeId, Employee employeeData)
        {
            using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
            {
                string query = @"UPDATE Employees 
                             SET first_name = @FirstName, 
                                 last_name = @LastName, 
                                 dob = @DateOfBirth, 
                                 gender = @Gender, 
                                 email = @Email, 
                                 phone_no = @PhoneNumber, 
                                 address = @Address, 
                                 position = @Position, 
                                 join_date = @JoiningDate, 
                                 Termin_Date = @TerminationDate 
                             WHERE employee_id = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", employeeData.FirstName);
                    command.Parameters.AddWithValue("@LastName", employeeData.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", employeeData.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", employeeData.Gender);
                    command.Parameters.AddWithValue("@Email", employeeData.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", employeeData.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", employeeData.Address);
                    command.Parameters.AddWithValue("@Position", employeeData.Position);
                    command.Parameters.AddWithValue("@JoiningDate", employeeData.JoiningDate);
                    command.Parameters.AddWithValue("@TerminationDate", (object)employeeData.TerminationDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Employee with ID {employeeId} updated successfully.");
                    }
                    else
                    {
                        throw new EmployeeNotFoundException("Employee Not found in the database");
                    }
                }
            }
        }

        public void PrintEmployeeDetail(Employee employee)
        {
            if (employee != null)
            {
                Console.WriteLine($"| Employee ID: {employee.EmployeeID} | " +
                                   $"Name: {employee.FirstName} {employee.LastName} | " +
                                   $"Date of Birth: {employee.DateOfBirth.ToShortDateString()} | " +
                                   $"Gender: {employee.Gender} | " +
                                   $"Email: {employee.Email} | " +
                                   $"Phone Number: {employee.PhoneNumber} | " +
                                   $"Address: {employee.Address} | \n" +
                                   $"Position: {employee.Position} | " +
                                   $"Joining Date: {employee.JoiningDate.ToShortDateString()} | " +
                                   $"Termination Date: {(employee.TerminationDate != null ? employee.TerminationDate.Value.ToShortDateString() : "Not terminated")} |");
            }
            else
            {
               //Console.ForegroundColor= ConsoleColor.Red;
                throw new EmployeeNotFoundException("Employee Not found in the database");
            }
        }

        public void GetEmployeeById(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
    
}
