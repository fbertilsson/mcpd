
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/13/2014 20:39:44
-- Generated from EDMX file: C:\arb\mcpd\data\ado\HierarchiesWithEF4\Hierarchies.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [hierarchies];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TptMammal_inherits_TptAnimal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TptAnimals_TptMammal] DROP CONSTRAINT [FK_TptMammal_inherits_TptAnimal];
GO
IF OBJECT_ID(N'[dbo].[FK_TptInsect_inherits_TptAnimal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TptAnimals_TptInsect] DROP CONSTRAINT [FK_TptInsect_inherits_TptAnimal];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TphAnimalSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TphAnimalSet];
GO
IF OBJECT_ID(N'[dbo].[TptAnimals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TptAnimals];
GO
IF OBJECT_ID(N'[dbo].[TptAnimals_TptMammal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TptAnimals_TptMammal];
GO
IF OBJECT_ID(N'[dbo].[TptAnimals_TptInsect]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TptAnimals_TptInsect];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TphAnimalSet'
CREATE TABLE [dbo].[TphAnimalSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [AntennaCount] int NULL,
    [DaysPregnancy] int NULL
);
GO

-- Creating table 'TptAnimals'
CREATE TABLE [dbo].[TptAnimals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TptAnimals_TptMammal'
CREATE TABLE [dbo].[TptAnimals_TptMammal] (
    [DaysPregnancy] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'TptAnimals_TptInsect'
CREATE TABLE [dbo].[TptAnimals_TptInsect] (
    [AntennaCount] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'TphAnimalSet'
ALTER TABLE [dbo].[TphAnimalSet]
ADD CONSTRAINT [PK_TphAnimalSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TptAnimals'
ALTER TABLE [dbo].[TptAnimals]
ADD CONSTRAINT [PK_TptAnimals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TptAnimals_TptMammal'
ALTER TABLE [dbo].[TptAnimals_TptMammal]
ADD CONSTRAINT [PK_TptAnimals_TptMammal]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TptAnimals_TptInsect'
ALTER TABLE [dbo].[TptAnimals_TptInsect]
ADD CONSTRAINT [PK_TptAnimals_TptInsect]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'TptAnimals_TptMammal'
ALTER TABLE [dbo].[TptAnimals_TptMammal]
ADD CONSTRAINT [FK_TptMammal_inherits_TptAnimal]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[TptAnimals]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'TptAnimals_TptInsect'
ALTER TABLE [dbo].[TptAnimals_TptInsect]
ADD CONSTRAINT [FK_TptInsect_inherits_TptAnimal]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[TptAnimals]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------