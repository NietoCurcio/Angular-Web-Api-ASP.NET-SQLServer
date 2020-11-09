
CREATE TABLE employee.Employee(
	EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
	EmployeeName NVARCHAR(100),
	DepartmentId INT FOREIGN KEY REFERENCES employee.Department(DepartmentId),
	DateOfJoining DATE,
	PhotoFileName NVARCHAR(100)
)

INSERT INTO employee.Employee (EmployeeName, DepartmentId, DateOfJoining, PhotoFileName) VALUES (N'SAM', 1, '2020-06-01', 'anonymous.png');

SELECT * FROM employee.Employee
INNER JOIN employee.Department
ON employee.Employee.DepartmentId= employee.department.DepartmentId;