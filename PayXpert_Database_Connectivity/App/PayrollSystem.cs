using PayXpert_Database_Connectivity.Exceptions;
using PayXpert_Database_Connectivity.Models;
using PayXpert_Database_Connectivity.Repository;
using PayXpert_Database_Connectivity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.App
{
    public class PayrollSystem
    {
        private readonly EmployeeService _employeeService;
        private readonly FinancialRecordService _financialRecordService;
        private readonly PayRollService _payRollService;
        private readonly TaxService _taxService;

        public PayrollSystem(EmployeeService employeeService, FinancialRecordService financialRecordService, PayRollService payRollService, TaxService taxService)
        {
            _employeeService = employeeService;
            _financialRecordService = financialRecordService;
            _payRollService = payRollService;
            _taxService = taxService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter here: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void Login()
        {
            Console.WriteLine("Logging in...");

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            RegistrationRepository registerRepo = new RegistrationRepository();
            bool loginSuccess = registerRepo.AuthenticateUser(email, password);

            if (loginSuccess)
            {
                Console.WriteLine("Login successful!");
                MainMenu();
                
            }
            else
            {
                Console.WriteLine("Login failed. Please check your email and password.");
            }
        }

        private void MainMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("*****************************************");
                Console.WriteLine("| Welcome to Employee Management System |");
                Console.WriteLine("*****************************************");
                Console.WriteLine("1. Employee Management");
                Console.WriteLine("2. Payroll Processing");
                Console.WriteLine("3. Tax Calculation");
                Console.WriteLine("4. Financial Reporting");
                Console.WriteLine("5. Exit");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EmployeeManagementMenu();
                        break;
                    case "2":
                        PayrollProcessingMenu();
                        break;
                    case "3":
                        TaxCalculationMenu();
                        break;
                    case "4":
                        FinancialReportingMenu();
                        break;
                    case "5":
                        Console.WriteLine("Exiting.....");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private void Register()
        {
            Console.WriteLine("Registering a new user...");

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            RegistrationRepository registrationRepo = new RegistrationRepository();
            bool registrationSuccess = registrationRepo.RegisterToSystem(username,password, email);

            if (registrationSuccess)
            {
                Console.WriteLine("User registration successful!");
            }
            else
            {
                Console.WriteLine("User registration failed. Please try again.");
            }
        }

        private void FinancialReportingMenu()
        {
            bool exit = false;
            do
            {
                Console.WriteLine("---Financial Reporting Menu---");
                Console.WriteLine("1. Add Financial Record");
                Console.WriteLine("2. Get Financial Record by ID");
                Console.WriteLine("3. Get Financial Records for Employee");
                Console.WriteLine("4. Get Financial Records for Date");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _financialRecordService.AddFinancialRecord();
                        break;
                    case "2":
                        _financialRecordService.GetFinancialRecordById();
                        break;
                    case "3":
                        _financialRecordService.GetFinancialRecordsForEmployee();
                        break;
                    case "4":
                        _financialRecordService.GetFinancialRecordsForDate();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            } while (!exit);
        }

       

        private void TaxCalculationMenu()
        {
        bool exit = false;
        do
        {
            Console.WriteLine("---Tax Calculation Menu---");
            Console.WriteLine("1. Calculate Tax");
            Console.WriteLine("2. Get Tax by ID");
            Console.WriteLine("3. Get Taxes for Employee");
            Console.WriteLine("4. Get Taxes for Year");
            Console.WriteLine("5. Back to Main Menu");

            Console.Write("Please enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _taxService.CalculateTax();
                    break;
                case "2":
                    _taxService.GetTaxById();
                    break;
                case "3":
                    _taxService.GetTaxesForEmployee();
                    break;
                case "4":
                    _taxService.GetTaxesForYear();
                    break;
                case "5":
                    exit = true; 
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (!exit);
    }

       

        private void PayrollProcessingMenu()
        {
            bool exit = false;
            do
            {
                Console.WriteLine("---Payroll Processing Menu---");
                Console.WriteLine("1. Generate Payroll");
                Console.WriteLine("2. Reterive Payroll for Id");
                Console.WriteLine("3. Get Payroll for Employee");
                Console.WriteLine("4. Get Payrolls for Period");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
     {
                    case "1":
                        _payRollService.GeneratePayroll();
                        break;
                    case "2":
                        _payRollService.GetPayrollById();
                        break;
                    case "3":
                        _payRollService.GetPayrollsForEmployee();
                        break;
                    case "4":
                         _payRollService.GetPayrollsForPeriod();
                        break;
                    case "5":
                        exit = true; 
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            } while (!exit);
        }

       

        private void EmployeeManagementMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("---Employee Management Menu---");
                Console.WriteLine("1. View Employee by ID");
                Console.WriteLine("2. Get All Employee's Details");
                Console.WriteLine("3. Add Employee");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Remove Employee");
                Console.WriteLine("6. Back to Main Menu");

                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _employeeService.GetEmployeeById();
                        break;
                    case "2":
                       _employeeService.GetAllEmployees();
                        break;
                    case "3":
                        int employeeId = _employeeService.AddEmployee();
                        Console.WriteLine($"The employee data wa inserted with ID : {employeeId}");
                        break;
                    case "4":
                        _employeeService.UpdateEmployee();
                        break;
                    case "5":
                        _employeeService.RemoveEmployee();
                        Console.WriteLine("Employee Removed");
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.---->");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
