CREATE DATABASE BlazorApp2;
USE BlazorApp2;


-- Creating the Identity schema
CREATE SCHEMA [Identity];
GO

-- Creating the AspNetUsers table
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
GO

-- Creating the AspNetRoles table
CREATE TABLE [Identity].[AspNetRoles]
(
    [Id] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(MAX),
    [NormalizedName] NVARCHAR(MAX),
    [ConcurrencyStamp] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

-- Creating the AspNetUserRoles table
CREATE TABLE [Identity].[AspNetUserRoles]
(
    [UserId] NVARCHAR(450) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) 
        REFERENCES [Identity].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) 
        REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

-- Creating the AspNetUserLogins table
CREATE TABLE [Identity].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR(128) NOT NULL,
    [ProviderKey] NVARCHAR(128) NOT NULL,
    [ProviderDisplayName] NVARCHAR(MAX),
    [UserId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) 
        REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

-- Creating the AspNetUserClaims table
CREATE TABLE [Identity].[AspNetUserClaims]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX),
    [ClaimValue] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) 
        REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

-- Creating the AspNetRoleClaims table
CREATE TABLE [Identity].[AspNetRoleClaims]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX),
    [ClaimValue] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) 
        REFERENCES [Identity].[AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

-- Creating the AspNetUserTokens table
CREATE TABLE [Identity].[AspNetUserTokens]
(
    [UserId] NVARCHAR(450) NOT NULL,
    [LoginProvider] NVARCHAR(128) NOT NULL,
    [Name] NVARCHAR(128) NOT NULL,
    [Value] NVARCHAR(MAX),
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) 
        REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO