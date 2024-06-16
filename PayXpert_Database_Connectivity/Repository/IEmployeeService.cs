using PayXpert_Database_Connectivity.Models;

namespace PayXpert_Database_Connectivity.Repository
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        int AddEmployee(Employee employeeData);
        void UpdateEmployee(int employeeId, Employee employeeData);
        void RemoveEmployee(int employeeId);
    }
}
