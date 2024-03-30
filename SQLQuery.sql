-- Create Department table
CREATE TABLE Department (
    DeptId INT PRIMARY KEY,
    DeptName NVARCHAR(100)
);

-- Insert dummy data into Department table
INSERT INTO Department (DeptId, DeptName) VALUES
(1, 'HR'),
(2, 'Finance'),
(3, 'IT');

-- Create Employee table
CREATE TABLE Employee (
    EmpId INT PRIMARY KEY IDENTITY,
    EmpCode NVARCHAR(50),
    Ename NVARCHAR(100),
    DeptId INT FOREIGN KEY REFERENCES Department(DeptId)
);

-- Insert dummy data into Employee table
INSERT INTO Employee (EmpCode, Ename, DeptId) VALUES
('E001', 'John Doe', 1),
('E002', 'Jane Smith', 2),
('E003', 'Michael Johnson', 3);
