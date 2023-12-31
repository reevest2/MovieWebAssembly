IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Core')
BEGIN
    EXEC('CREATE SCHEMA Core;');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Core].[HotelRooms]') AND type = N'U')
BEGIN
    CREATE TABLE [Core].[HotelRooms] (
        Id INT IDENTITY PRIMARY KEY,
        Name NVARCHAR(255),
        Occupancy INT,
        RegularRate FLOAT,
        Details NVARCHAR(MAX),
        SqFt NVARCHAR(255),
        CreatedBy NVARCHAR(255),
        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
        UpdatedBy NVARCHAR(255),
        UpdatedDate DATETIME
    );
END
GO
