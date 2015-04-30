
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/29/2015 21:39:04
-- Generated from EDMX file: D:\dars\c# projects\clientFactory\clientFactory\clientModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ClientDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ReportReportCategory_Report]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReportReportCategory] DROP CONSTRAINT [FK_ReportReportCategory_Report];
GO
IF OBJECT_ID(N'[dbo].[FK_ReportReportCategory_ReportCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReportReportCategory] DROP CONSTRAINT [FK_ReportReportCategory_ReportCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ReportAttachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reports] DROP CONSTRAINT [FK_ReportAttachments];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Reports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reports];
GO
IF OBJECT_ID(N'[dbo].[Attachments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachments];
GO
IF OBJECT_ID(N'[dbo].[ReportCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReportCategories];
GO
IF OBJECT_ID(N'[dbo].[ReportReportCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReportReportCategory];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [RoleId] int  NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [Age] smallint  NOT NULL,
    [EmploymentDate] datetime  NOT NULL,
    [Resume] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [AvatarLocation] nvarchar(max)  NOT NULL,
    [NationalId] int  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Gender] bit  NOT NULL
);
GO

-- Creating table 'Reports'
CREATE TABLE [dbo].[Reports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sender_ID] int  NOT NULL,
    [Sender] nvarchar(max)  NOT NULL,
    [Recipient_ID] int  NOT NULL,
    [Recipient] nvarchar(max)  NOT NULL,
    [SendDate] datetime  NOT NULL,
    [isRead] bit  NOT NULL,
    [isMark] bit  NOT NULL,
    [Attachment_Id] int  NOT NULL
);
GO

-- Creating table 'Attachments'
CREATE TABLE [dbo].[Attachments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileLocation] nvarchar(max)  NOT NULL,
    [uploadTime] time  NOT NULL
);
GO

-- Creating table 'ReportCategories'
CREATE TABLE [dbo].[ReportCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ReportReportCategory'
CREATE TABLE [dbo].[ReportReportCategory] (
    [Reports_Id] int  NOT NULL,
    [ReportCategories_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [PK_Reports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attachments'
ALTER TABLE [dbo].[Attachments]
ADD CONSTRAINT [PK_Attachments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReportCategories'
ALTER TABLE [dbo].[ReportCategories]
ADD CONSTRAINT [PK_ReportCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Reports_Id], [ReportCategories_Id] in table 'ReportReportCategory'
ALTER TABLE [dbo].[ReportReportCategory]
ADD CONSTRAINT [PK_ReportReportCategory]
    PRIMARY KEY CLUSTERED ([Reports_Id], [ReportCategories_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Reports_Id] in table 'ReportReportCategory'
ALTER TABLE [dbo].[ReportReportCategory]
ADD CONSTRAINT [FK_ReportReportCategory_Report]
    FOREIGN KEY ([Reports_Id])
    REFERENCES [dbo].[Reports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReportCategories_Id] in table 'ReportReportCategory'
ALTER TABLE [dbo].[ReportReportCategory]
ADD CONSTRAINT [FK_ReportReportCategory_ReportCategory]
    FOREIGN KEY ([ReportCategories_Id])
    REFERENCES [dbo].[ReportCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReportReportCategory_ReportCategory'
CREATE INDEX [IX_FK_ReportReportCategory_ReportCategory]
ON [dbo].[ReportReportCategory]
    ([ReportCategories_Id]);
GO

-- Creating foreign key on [Attachment_Id] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_ReportAttachments]
    FOREIGN KEY ([Attachment_Id])
    REFERENCES [dbo].[Attachments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReportAttachments'
CREATE INDEX [IX_FK_ReportAttachments]
ON [dbo].[Reports]
    ([Attachment_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------