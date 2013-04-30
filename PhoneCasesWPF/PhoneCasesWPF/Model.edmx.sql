
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/30/2013 14:45:31
-- Generated from EDMX file: E:\ProgrammingProjects\PhoneCasesWPF\PhoneCasesWPF\PhoneCasesWPF\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PhoneCases];
GO
IF SCHEMA_ID(N'pc') IS NULL EXECUTE(N'CREATE SCHEMA [pc]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[pc].[FK_CompaniesCustomers]', 'F') IS NOT NULL
    ALTER TABLE [pc].[Customers] DROP CONSTRAINT [FK_CompaniesCustomers];
GO
IF OBJECT_ID(N'[pc].[FK_LocationsCompanies]', 'F') IS NOT NULL
    ALTER TABLE [pc].[Companies] DROP CONSTRAINT [FK_LocationsCompanies];
GO
IF OBJECT_ID(N'[pc].[FK_UsersCases]', 'F') IS NOT NULL
    ALTER TABLE [pc].[Cases] DROP CONSTRAINT [FK_UsersCases];
GO
IF OBJECT_ID(N'[pc].[FK_TagsCaseTags]', 'F') IS NOT NULL
    ALTER TABLE [pc].[CaseTags] DROP CONSTRAINT [FK_TagsCaseTags];
GO
IF OBJECT_ID(N'[pc].[FK_CasesCaseTags]', 'F') IS NOT NULL
    ALTER TABLE [pc].[CaseTags] DROP CONSTRAINT [FK_CasesCaseTags];
GO
IF OBJECT_ID(N'[pc].[FK_CustomersCases]', 'F') IS NOT NULL
    ALTER TABLE [pc].[Cases] DROP CONSTRAINT [FK_CustomersCases];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[pc].[Companies]', 'U') IS NOT NULL
    DROP TABLE [pc].[Companies];
GO
IF OBJECT_ID(N'[pc].[Users]', 'U') IS NOT NULL
    DROP TABLE [pc].[Users];
GO
IF OBJECT_ID(N'[pc].[Customers]', 'U') IS NOT NULL
    DROP TABLE [pc].[Customers];
GO
IF OBJECT_ID(N'[pc].[Cases]', 'U') IS NOT NULL
    DROP TABLE [pc].[Cases];
GO
IF OBJECT_ID(N'[pc].[Locations]', 'U') IS NOT NULL
    DROP TABLE [pc].[Locations];
GO
IF OBJECT_ID(N'[pc].[CaseTags]', 'U') IS NOT NULL
    DROP TABLE [pc].[CaseTags];
GO
IF OBJECT_ID(N'[pc].[Tags]', 'U') IS NOT NULL
    DROP TABLE [pc].[Tags];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Companies'
CREATE TABLE [pc].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LocationId] int  NOT NULL,
    [CompanyTypesId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [pc].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [pc].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'Cases'
CREATE TABLE [pc].[Cases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CustomerId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Info] nvarchar(max)  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [EndTime] datetime  NULL,
    [TotalTime] datetime  NULL,
    [CloseTime] datetime  NULL,
    [Reconnect] bit  NOT NULL,
    [HighPrio] bit  NOT NULL,
    [Closed] bit  NOT NULL,
    [Active] bit  NOT NULL,
    [CustomersId] int  NOT NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [pc].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CaseTags'
CREATE TABLE [pc].[CaseTags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TagsId] int  NOT NULL,
    [CasesId] int  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [pc].[Tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CompanyTypes'
CREATE TABLE [pc].[CompanyTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [pc].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [pc].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [pc].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cases'
ALTER TABLE [pc].[Cases]
ADD CONSTRAINT [PK_Cases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [pc].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CaseTags'
ALTER TABLE [pc].[CaseTags]
ADD CONSTRAINT [PK_CaseTags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tags'
ALTER TABLE [pc].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompanyTypes'
ALTER TABLE [pc].[CompanyTypes]
ADD CONSTRAINT [PK_CompanyTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CompanyId] in table 'Customers'
ALTER TABLE [pc].[Customers]
ADD CONSTRAINT [FK_CompaniesCustomers]
    FOREIGN KEY ([CompanyId])
    REFERENCES [pc].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompaniesCustomers'
CREATE INDEX [IX_FK_CompaniesCustomers]
ON [pc].[Customers]
    ([CompanyId]);
GO

-- Creating foreign key on [LocationId] in table 'Companies'
ALTER TABLE [pc].[Companies]
ADD CONSTRAINT [FK_LocationsCompanies]
    FOREIGN KEY ([LocationId])
    REFERENCES [pc].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationsCompanies'
CREATE INDEX [IX_FK_LocationsCompanies]
ON [pc].[Companies]
    ([LocationId]);
GO

-- Creating foreign key on [UserId] in table 'Cases'
ALTER TABLE [pc].[Cases]
ADD CONSTRAINT [FK_UsersCases]
    FOREIGN KEY ([UserId])
    REFERENCES [pc].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersCases'
CREATE INDEX [IX_FK_UsersCases]
ON [pc].[Cases]
    ([UserId]);
GO

-- Creating foreign key on [TagsId] in table 'CaseTags'
ALTER TABLE [pc].[CaseTags]
ADD CONSTRAINT [FK_TagsCaseTags]
    FOREIGN KEY ([TagsId])
    REFERENCES [pc].[Tags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TagsCaseTags'
CREATE INDEX [IX_FK_TagsCaseTags]
ON [pc].[CaseTags]
    ([TagsId]);
GO

-- Creating foreign key on [CasesId] in table 'CaseTags'
ALTER TABLE [pc].[CaseTags]
ADD CONSTRAINT [FK_CasesCaseTags]
    FOREIGN KEY ([CasesId])
    REFERENCES [pc].[Cases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CasesCaseTags'
CREATE INDEX [IX_FK_CasesCaseTags]
ON [pc].[CaseTags]
    ([CasesId]);
GO

-- Creating foreign key on [CustomersId] in table 'Cases'
ALTER TABLE [pc].[Cases]
ADD CONSTRAINT [FK_CustomersCases]
    FOREIGN KEY ([CustomersId])
    REFERENCES [pc].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomersCases'
CREATE INDEX [IX_FK_CustomersCases]
ON [pc].[Cases]
    ([CustomersId]);
GO

-- Creating foreign key on [CompanyTypesId] in table 'Companies'
ALTER TABLE [pc].[Companies]
ADD CONSTRAINT [FK_CompanyTypesCompanies]
    FOREIGN KEY ([CompanyTypesId])
    REFERENCES [pc].[CompanyTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyTypesCompanies'
CREATE INDEX [IX_FK_CompanyTypesCompanies]
ON [pc].[Companies]
    ([CompanyTypesId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------