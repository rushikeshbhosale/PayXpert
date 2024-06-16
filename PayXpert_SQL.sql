CREATE DATABASE PayXpertDB
USE PayXpertDB

-- Creating Employee Table
CREATE TABLE Employees (
    employee_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    dob DATE,
    gender VARCHAR(10),
    email VARCHAR(100),
    phone_no VARCHAR(20),
    address VARCHAR(255),
    position VARCHAR(100),
    join_date DATE,
    Termin_Date DATE NULL
)

--Creating Payroll Table
CREATE TABLE Payroll (
    payroll_id INT IDENTITY(1,1) PRIMARY KEY,
    employee_id INT,
    payPeriodStartDate DATE,
    payPeriodEndDate DATE,
    basic_salary DECIMAL(10, 2),
    ot_pay DECIMAL(10, 2),
    deductions DECIMAL(10, 2),
    net_salary DECIMAL(10, 2),
    FOREIGN KEY (employee_id) REFERENCES Employees(employee_id)
)

-- Creating Tax Table
CREATE TABLE Tax (
    tax_id INT IDENTITY(1,1) PRIMARY KEY,
    employee_id INT,
    tax_year INT,
    taxable_income DECIMAL(10, 2),
    tax_amount DECIMAL(10, 2),
    FOREIGN KEY (employee_id) REFERENCES Employees (employee_id)
)

-- Creating FinancialRecord Table
CREATE TABLE FinancialRecord (
    record_id INT IDENTITY(1,1) PRIMARY KEY,
    employee_id INT,
    record_date DATE,
    descriptions VARCHAR(255),
    amount DECIMAL(10, 2),
    record_type VARCHAR(50),
    FOREIGN KEY (employee_id) REFERENCES Employees(employee_id)
)
-- Inserting Values into Employee Table
INSERT INTO Employees (first_name, last_name, dob, gender, email, phone_no, address, position, join_date)
VALUES
  ('John', 'Doe', '1990-01-01', 'Male', 'john.doe@company.com', '1234567890', '123 Main St, Anytown, CA 12345', 'Software Engineer', '2023-01-15'),
  ('Jane', 'Smith', '1985-07-12', 'Female', 'jane.smith@company.com', '9876543210', '456 Elm St, Anytown, CA 12345', 'Marketing Manager', '2022-06-01'),
  ('Michael', 'Lee', '1995-03-24', 'Male', 'michael.lee@company.com', '5551234567', '789 Oak St, Anytown, CA 12345', 'Sales Representative', '2023-11-05'),
  ('Olivia', 'Garcia', '1988-10-21', 'Female', 'olivia.garcia@company.com', '7894561230', '1011 Pine St, Anytown, CA 12345', 'Accountant', '2021-05-12'),
  ('David', 'Johnson', '1992-04-08', 'Male', 'david.johnson@company.com', '3210987654', '1212 Maple St, Anytown, CA 12345', 'Customer Service Representative', '2024-02-19')
SELECT * FROM Employees

-- Inserting Into PayRoll
INSERT INTO Payroll (employee_id, payPeriodStartDate, payPeriodEndDate, basic_salary, ot_pay, deductions, net_salary)
VALUES
  (1, '2024-02-05', '2024-02-18', 3500.00, 100.00, 200.00, 3400.00), 
  (2, '2024-02-05', '2024-02-18', 4200.00, 50.00, 150.00, 4100.00), 
  (3, '2024-02-05', '2024-02-18', 2800.00, 75.00, 120.00, 2755.00), 
  (4, '2024-02-05', '2024-02-18', 3800.00, 120.00, 180.00, 3740.00),
  (5, '2024-02-05', '2024-02-18', 3200.00, 85.00, 140.00, 3145.00)
SELECT * FROM Payroll

--Inserting Into Tax table
INSERT INTO Tax (employee_id, tax_year, taxable_income, tax_amount)
VALUES
  (1, 2023, 1000.00, 150.00),
  (2, 2023, 1500.00, 225.00),
  (3, 2023, 800.00, 120.00),
  (4, 2023, 1200.00, 180.00),
  (5, 2023, 900.00, 135.00)
SELECT * FROM Tax

--Inserting Into FinancialRecord
INSERT INTO FinancialRecord (employee_id, record_date, descriptions, amount, record_type)
VALUES
  (1, '2024-02-21', 'Salary Payment', 3500.00, 'Income'),
  (2, '2024-02-22', 'Travel Reimbursement', 150.00, 'Income'),
  (3, '2024-02-23', 'Office Supplies Purchase', -200.00, 'Expense'),
  (4, '2024-02-24', 'Software Subscription', -50.00, 'Expense'),
  (5, '2024-02-24', 'Bonus Payment', 200.00, 'Income')

  --Queries--
 --1. Retrieve all employees' full names and positions:
 SELECT 
	concat_ws(' ', first_name, last_name) AS FullName,
	position
 FROM Employees

 --2. Calculate the total deductions for each employee for a specific pay period:
 SELECT 
	employee_id,
	concat_ws(' ', first_name, last_name) AS EmpName,
	(
		SELECT SUM(deductions) FROM Payroll p 
		WHERE p.employee_id=e.employee_id
		AND p.payPeriodStartDate ='2024-02-15'
		AND p.payPeriodEndDate='2024-02-18'
	) AS [Total Deduction]
FROM Employees e
GROUP BY employee_id, concat_ws(' ', first_name, last_name) 

--3. Retrieve the employee with the highest basic salary:
SELECT TOP 1 
	e.employee_id,
	concat_ws(' ', e.first_name, e.last_name) AS EmpName,
	p.basic_salary
FROM Employees e
JOIN Payroll p 
ON p.employee_id = e.employee_id
ORDER BY p.basic_salary desc

--4. Calculate the total tax paid by each employee for a specific tax year:
SELECT 
	e.employee_id, 
	concat_ws(' ', e.first_name, e.last_name) AS EmpName,
	t.tax_year,
	(SELECT SUM(t.tax_amount) FROM Tax t where e.employee_id= t.employee_id )
FROM Employees e, Tax t
GROUP BY tax_year,e.employee_id, concat_ws(' ', e.first_name, e.last_name)
HAVING tax_year=2022

--4.Retrieve the financial records for a specific employee within a date range:
DECLARE @emp_id int = 2
DECLARE @from date = '2024-02-21'
DECLARE @to date = '2024-02-23'

SELECT 
	e.employee_id,
	e.first_name, 
	e.last_name,
	f.record_id,
	f.record_type,
	f.record_type,
	f.descriptions,
	f.descriptions
	
FROM Employees e, FinancialRecord f
WHERE e.employee_id = @emp_id 
	AND record_date BETWEEN @from AND @to 

--5. Retrieve the employee(s) who have been terminated:
SELECT * FROM Employees
WHERE Termin_Date IS NOT NULL

-- 6. Retrive employees(s) who lives in same residence:
SELECT employee_id, CONCAT(first_name, ' ',last_name) AS FullName, Address
FROM Employees
WHERE Address IN (
    SELECT Address
    FROM Employees
    GROUP BY Address
    HAVING COUNT(*) > 1
)

--FUNCTIONALITIES:
--1. Employee Management (CRUD operations):
--Create(INSERT)
INSERT INTO Employees (first_name, last_name, dob, gender, email, phone_no, address, position, join_date)
VALUES ('Roronoa', 'Zoro', '1990-11-11', 'Male', 'roronoa@example.com', '1234567890', '123 Main St', 'Manager', '2023-01-01');
--Read(SELECT)
SELECT * FROM Employees
--Update(UPDATE)
UPDATE Employees
SET position = 'Senior Manager' 
WHERE employee_id = 6
--Delete(DELETE)
DELETE FROM Employees
Where employee_id = 6

--2. PayRoll Processing
DECLARE @PayRoll Table(
	emp_id int, FullName varchar(MAX), Net_Salary decimal(10,2)
	)

INSERT INTO @PayRoll(emp_id, FullName, Net_Salary)
SELECT 
	e.employee_id,
	CONCAT(e.first_name,' ', e.last_name),
	(p.basic_salary+p.ot_pay-p.deductions) AS Net_salary
FROM Employees e, Payroll p
WHERE p.employee_id = e.employee_id

SELECT * FROM @PayRoll






