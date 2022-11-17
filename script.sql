CREATE DATABASE ISRPO_LAB_Ashrafzianov;

use ISRPO_LAB_Ashrafzianov;

CREATE TABLE [Data] (
	ID INT PRIMARY KEY,
	FullName NVARCHAR(100),
	BirthDate DATE,
	PostCode NVARCHAR(10),
	City NVARCHAR(20),
	Street NVARCHAR(20),
	BuildingNumber INT,
	AppartmentNumber INT,
	Email NVARCHAR(100)
);
