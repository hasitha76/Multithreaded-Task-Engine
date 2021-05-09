CREATE DATABASE SEBTest;

GO
USE SEBTest
GO

CREATE TABLE Marks (
    Id uniqueidentifier,
    Score int
);
GO

CREATE TABLE Audits (
    Id uniqueidentifier,
    Total int,
	Who varchar(50),
	Created Datetime,
	Filters varchar(200)
);
GO