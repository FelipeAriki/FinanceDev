-- Crie o banco FinanceDb no seu servidor SQL Server (via SSMS)
USE [master]; CREATE DATABASE FinanceDb;
USE FinanceDb;
GO
IF OBJECT_ID('dbo.Categories','U') IS NOT NULL DROP TABLE dbo.Categories;
IF OBJECT_ID('dbo.Transactions','U') IS NOT NULL DROP TABLE dbo.Transactions;
CREATE TABLE Categories (
 Id INT IDENTITY PRIMARY KEY,
 Name VARCHAR(100) NOT NULL,
 Color VARCHAR(20) NULL
);
CREATE TABLE Transactions (
 Id INT IDENTITY PRIMARY KEY,
 Type CHAR(1) NOT NULL, -- 'R' receita / 'D' despesa
 CategoryId INT NOT NULL,
 Description VARCHAR(200) NULL,
 Amount DECIMAL(18,2) NOT NULL,
 Date DATETIME NOT NULL,
 CONSTRAINT FK_Transactions_Categories FOREIGN KEY (CategoryId)
 REFERENCES Categories(Id)
);
-- Seed categories
INSERT INTO Categories (Name, Color) VALUES
('Salário', '#4CAF50'),
('Alimentação', '#FF9800'),
('Transporte', '#03A9F4'),
('Lazer', '#9C27B0'),
('Outros', '#607D8B');
-- Seed transactions (um mês de exemplo)
INSERT INTO Transactions (Type, CategoryId, Description, Amount, Date) VALUES
('R', 1, 'Salário Novembro', 4500.00, '20251101'),
('D', 2, 'Supermercado', 350.75, '20251105'),
('D', 3, 'Combustível', 120.00, '20251107'),
('D', 4, 'Cinema', 45.00, '20251109'),
('R', 5, 'Venda pontual', 200.00, '20251110'),
('D', 2, 'Almoço', 28.50, '20251112'),
('D', 2, 'Supermercado', 412.30, '20251120'),
('R', 1, 'Freelance', 800.00, '20251028'),
('D', 3, 'Uber', 40.00, '20251030');
GO

SELECT * FROM categories
select * from transactions