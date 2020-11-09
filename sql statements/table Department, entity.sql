USE EmployeeDB;
GO

CREATE SCHEMA employee;
GO

CREATE TABLE employee.Department(
	DepartmentId INT IDENTITY(1,1) primary key,
	DepartmentName NVARCHAR(100)	
)
GO

DROP TABLE employee.Department;

INSERT INTO employee.Department (DepartmentName) VALUES (N'IT');

INSERT INTO employee.Department (DepartmentName) VALUES (N'Support');

SELECT * FROM employee.Department;