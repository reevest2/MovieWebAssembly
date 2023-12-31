IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'MovieWebAssemblyAppDatabase')
BEGIN
    CREATE DATABASE [MovieWebAssemblyAppDatabase];
END
GO

USE [MovieWebAssemblyAppDatabase];
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Identity')
BEGIN
EXEC('CREATE SCHEMA [Identity]');
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Identity].[AspNetUsers]') AND type = N'U')
BEGIN
CREATE TABLE [Identity].[AspNetUsers]
(
    [Id] NVARCHAR(450) NOT NULL,
    [UserName] NVARCHAR(256) NULL,
    [NormalizedUserName] NVARCHAR(256) NULL,
    [Email] NVARCHAR(256) NULL,
    [NormalizedEmail] NVARCHAR(256) NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(MAX) NULL,
    [SecurityStamp] NVARCHAR(MAX) NULL,
    [ConcurrencyStamp] NVARCHAR(MAX) NULL,
    [PhoneNumber] NVARCHAR(MAX) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL,
    [TwoFactorEnabled] BIT NOT NULL,
    [LockoutEnd] DATETIMEOFFSET NULL,
    [LockoutEnabled] BIT NOT NULL,
    [AccessFailedCount] INT NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Identity].[AspNetRoles]') AND type = N'U')
BEGIN
CREATE TABLE [Identity].[AspNetRoles]
(
    [Id] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(MAX),
    [NormalizedName] NVARCHAR(MAX),
    [ConcurrencyStamp] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Identity].[AspNetUserRoles]') AND type = N'U')
BEGIN
CREATE TABLE [Identity].[AspNetUserRoles]
(
    [UserId] NVARCHAR(450) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Identity].[AspNetUserLogins]') AND type = N'U')
BEGIN
CREATE TABLE [Identity].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR(128) NOT NULL,
    [ProviderKey] NVARCHAR(128) NOT NULL,
    [ProviderDisplayName] NVARCHAR(MAX),
    [UserId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Identity].[AspNetUserClaims]') AND type = N'U')
BEGIN
CREATE TABLE [Identity].[AspNetUserClaims]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX),
    [ClaimValue] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Identity].[AspNetRoleClaims]') AND type = N'U')
BEGIN
CREATE TABLE [Identity].[AspNetRoleClaims]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX),
    [ClaimValue] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Identity].[AspNetUserTokens]') AND type = N'U')
BEGIN
CREATE TABLE [Identity].[AspNetUserTokens]
(
    [UserId] NVARCHAR(450) NOT NULL,
    [LoginProvider] NVARCHAR(128) NOT NULL,
    [Name] NVARCHAR(128) NOT NULL,
    [Value] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END
GO

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

MERGE INTO [Core].[HotelRooms] AS target
USING (
    VALUES
        ('Room 101', 2, 150.00, 'Standard room with a double bed', '250 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        ('Room 202', 4, 250.00, 'Deluxe room with two queen beds and a balcony', '400 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        ('Room 303', 1, 100.00, 'Single room with a twin bed', '200 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE()),
        ('Room 404', 3, 200.00, 'Premium room with a king bed and a Jacuzzi', '500 sq.ft', 'John Doe', GETDATE(), 'Jane Smith', GETDATE())
) AS source (Name, Occupancy, RegularRate, Details, SqFt, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
ON target.Name = source.Name
WHEN NOT MATCHED THEN
    INSERT (Name, Occupancy, RegularRate, Details, SqFt, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
    VALUES (source.Name, source.Occupancy, source.RegularRate, source.Details, source.SqFt, source.CreatedBy, source.CreatedDate, source.UpdatedBy, source.UpdatedDate);
GO

