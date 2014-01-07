
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/19/2013 23:39:08
-- Generated from EDMX file: C:\arb\mcpd\data\ado\EntityFrameworkTest1\Animals.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Animals];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AnimalHabitat_Animal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalHabitat] DROP CONSTRAINT [FK_AnimalHabitat_Animal];
GO
IF OBJECT_ID(N'[dbo].[FK_AnimalHabitat_Habitat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalHabitat] DROP CONSTRAINT [FK_AnimalHabitat_Habitat];
GO
IF OBJECT_ID(N'[dbo].[FK_Dog_inherits_Animal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalSet_Dog] DROP CONSTRAINT [FK_Dog_inherits_Animal];
GO
IF OBJECT_ID(N'[dbo].[FK_Monkey_inherits_Animal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnimalSet_Monkey] DROP CONSTRAINT [FK_Monkey_inherits_Animal];
GO
IF OBJECT_ID(N'[dbo].[FK_Instructor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet] DROP CONSTRAINT [FK_Instructor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet] DROP CONSTRAINT [FK_Student_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AnimalSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnimalSet];
GO
IF OBJECT_ID(N'[dbo].[HabitatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HabitatSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[AnimalSet_Dog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnimalSet_Dog];
GO
IF OBJECT_ID(N'[dbo].[AnimalSet_Monkey]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnimalSet_Monkey];
GO
IF OBJECT_ID(N'[dbo].[AnimalHabitat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnimalHabitat];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AnimalSet'
CREATE TABLE [dbo].[AnimalSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RaceName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HabitatSet'
CREATE TABLE [dbo].[HabitatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'AnimalSet_Dog'
CREATE TABLE [dbo].[AnimalSet_Dog] (
    [PetName] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'AnimalSet_Monkey'
CREATE TABLE [dbo].[AnimalSet_Monkey] (
    [MonkeySpecial] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet_Instructor'
CREATE TABLE [dbo].[PersonSet_Instructor] (
    [HireDate] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet_Student'
CREATE TABLE [dbo].[PersonSet_Student] (
    [EnrollmentDate] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'AnimalHabitat'
CREATE TABLE [dbo].[AnimalHabitat] (
    [Animal_Id] int  NOT NULL,
    [Habitat_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AnimalSet'
ALTER TABLE [dbo].[AnimalSet]
ADD CONSTRAINT [PK_AnimalSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HabitatSet'
ALTER TABLE [dbo].[HabitatSet]
ADD CONSTRAINT [PK_HabitatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnimalSet_Dog'
ALTER TABLE [dbo].[AnimalSet_Dog]
ADD CONSTRAINT [PK_AnimalSet_Dog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnimalSet_Monkey'
ALTER TABLE [dbo].[AnimalSet_Monkey]
ADD CONSTRAINT [PK_AnimalSet_Monkey]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Instructor'
ALTER TABLE [dbo].[PersonSet_Instructor]
ADD CONSTRAINT [PK_PersonSet_Instructor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Student'
ALTER TABLE [dbo].[PersonSet_Student]
ADD CONSTRAINT [PK_PersonSet_Student]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Animal_Id], [Habitat_Id] in table 'AnimalHabitat'
ALTER TABLE [dbo].[AnimalHabitat]
ADD CONSTRAINT [PK_AnimalHabitat]
    PRIMARY KEY NONCLUSTERED ([Animal_Id], [Habitat_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Animal_Id] in table 'AnimalHabitat'
ALTER TABLE [dbo].[AnimalHabitat]
ADD CONSTRAINT [FK_AnimalHabitat_Animal]
    FOREIGN KEY ([Animal_Id])
    REFERENCES [dbo].[AnimalSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Habitat_Id] in table 'AnimalHabitat'
ALTER TABLE [dbo].[AnimalHabitat]
ADD CONSTRAINT [FK_AnimalHabitat_Habitat]
    FOREIGN KEY ([Habitat_Id])
    REFERENCES [dbo].[HabitatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AnimalHabitat_Habitat'
CREATE INDEX [IX_FK_AnimalHabitat_Habitat]
ON [dbo].[AnimalHabitat]
    ([Habitat_Id]);
GO

-- Creating foreign key on [Id] in table 'AnimalSet_Dog'
ALTER TABLE [dbo].[AnimalSet_Dog]
ADD CONSTRAINT [FK_Dog_inherits_Animal]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[AnimalSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'AnimalSet_Monkey'
ALTER TABLE [dbo].[AnimalSet_Monkey]
ADD CONSTRAINT [FK_Monkey_inherits_Animal]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[AnimalSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PersonSet_Instructor'
ALTER TABLE [dbo].[PersonSet_Instructor]
ADD CONSTRAINT [FK_Instructor_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PersonSet_Student'
ALTER TABLE [dbo].[PersonSet_Student]
ADD CONSTRAINT [FK_Student_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------