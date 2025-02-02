CREATE DATABASE FirmaKanc;
GO

USE FirmaKanc;
GO

CREATE TABLE ProductTypes (
    TypeID INT IDENTITY PRIMARY KEY,
    TypeName NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Suppliers (
    SupplierID INT IDENTITY PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Products (
    ProductID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    TypeID INT NOT NULL,
    SupplierID INT NOT NULL,
    Quantity INT NOT NULL,
    CostPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (TypeID) REFERENCES ProductTypes(TypeID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);

CREATE TABLE SalesManagers (
    ManagerID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE CustomerCompanies (
    CompanyID INT IDENTITY PRIMARY KEY,
    CompanyName NVARCHAR(100) NOT NULL UNIQUE
);

-- Insert sample data
INSERT INTO ProductTypes (TypeName) VALUES ('Pens'), ('Notebooks'), ('Folders');
INSERT INTO Suppliers (SupplierName) VALUES ('OfficeMax'), ('Staples'), ('Local Supply Co.');
INSERT INTO Products (Name, TypeID, SupplierID, Quantity, CostPrice) VALUES
('Blue Pen', 1, 1, 100, 1.20),
('A4 Notebook', 2, 2, 50, 2.50);
INSERT INTO SalesManagers (Name, Email) VALUES ('John Doe', 'john@example.com');
INSERT INTO CustomerCompanies (CompanyName) VALUES ('ABC Corp'), ('XYZ Ltd');