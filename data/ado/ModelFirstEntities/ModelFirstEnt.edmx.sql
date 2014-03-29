
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/29/2014 12:09:08
-- Generated from EDMX file: C:\arb\mcpd\data\ado\ModelFirstEntities\ModelFirstEnt.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ModelFirstEntities];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Sub1A_inherits_BaseEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BaseEntitySet_Sub1A] DROP CONSTRAINT [FK_Sub1A_inherits_BaseEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_Sub1B_inherits_BaseEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BaseEntitySet_Sub1B] DROP CONSTRAINT [FK_Sub1B_inherits_BaseEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_TphSub1A_inherits_TphBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TphBaseSet_TphSub1A] DROP CONSTRAINT [FK_TphSub1A_inherits_TphBase];
GO
IF OBJECT_ID(N'[dbo].[FK_TphSub1B_inherits_TphBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TphBaseSet_TphSub1B] DROP CONSTRAINT [FK_TphSub1B_inherits_TphBase];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BaseEntitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BaseEntitySet];
GO
IF OBJECT_ID(N'[dbo].[TphBaseSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TphBaseSet];
GO
IF OBJECT_ID(N'[dbo].[BaseEntitySet_Sub1A]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BaseEntitySet_Sub1A];
GO
IF OBJECT_ID(N'[dbo].[BaseEntitySet_Sub1B]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BaseEntitySet_Sub1B];
GO
IF OBJECT_ID(N'[dbo].[TphBaseSet_TphSub1A]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TphBaseSet_TphSub1A];
GO
IF OBJECT_ID(N'[dbo].[TphBaseSet_TphSub1B]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TphBaseSet_TphSub1B];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BaseEntitySet'
CREATE TABLE [dbo].[BaseEntitySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Manufacturer] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TphBaseSet'
CREATE TABLE [dbo].[TphBaseSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Manufacturer] nvarchar(max)  NOT NULL,
    [MyDiscriminator] nvarchar(max)  NULL
);
GO

-- Creating table 'BaseEntitySet_Sub1A'
CREATE TABLE [dbo].[BaseEntitySet_Sub1A] (
    [SpecialA] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'BaseEntitySet_Sub1B'
CREATE TABLE [dbo].[BaseEntitySet_Sub1B] (
    [SpecialB] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'TphBaseSet_TphSub1A'
CREATE TABLE [dbo].[TphBaseSet_TphSub1A] (
    [SpecialA] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'TphBaseSet_TphSub1B'
CREATE TABLE [dbo].[TphBaseSet_TphSub1B] (
    [SpecialB] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BaseEntitySet'
ALTER TABLE [dbo].[BaseEntitySet]
ADD CONSTRAINT [PK_BaseEntitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TphBaseSet'
ALTER TABLE [dbo].[TphBaseSet]
ADD CONSTRAINT [PK_TphBaseSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BaseEntitySet_Sub1A'
ALTER TABLE [dbo].[BaseEntitySet_Sub1A]
ADD CONSTRAINT [PK_BaseEntitySet_Sub1A]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BaseEntitySet_Sub1B'
ALTER TABLE [dbo].[BaseEntitySet_Sub1B]
ADD CONSTRAINT [PK_BaseEntitySet_Sub1B]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TphBaseSet_TphSub1A'
ALTER TABLE [dbo].[TphBaseSet_TphSub1A]
ADD CONSTRAINT [PK_TphBaseSet_TphSub1A]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TphBaseSet_TphSub1B'
ALTER TABLE [dbo].[TphBaseSet_TphSub1B]
ADD CONSTRAINT [PK_TphBaseSet_TphSub1B]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'BaseEntitySet_Sub1A'
ALTER TABLE [dbo].[BaseEntitySet_Sub1A]
ADD CONSTRAINT [FK_Sub1A_inherits_BaseEntity]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[BaseEntitySet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'BaseEntitySet_Sub1B'
ALTER TABLE [dbo].[BaseEntitySet_Sub1B]
ADD CONSTRAINT [FK_Sub1B_inherits_BaseEntity]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[BaseEntitySet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'TphBaseSet_TphSub1A'
ALTER TABLE [dbo].[TphBaseSet_TphSub1A]
ADD CONSTRAINT [FK_TphSub1A_inherits_TphBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[TphBaseSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'TphBaseSet_TphSub1B'
ALTER TABLE [dbo].[TphBaseSet_TphSub1B]
ADD CONSTRAINT [FK_TphSub1B_inherits_TphBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[TphBaseSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------