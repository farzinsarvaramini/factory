
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/08/2015 23:28:14
-- Generated from EDMX file: C:\Users\farzin\Documents\GitHub\factory\Integrating and testing\clientFactory\Models\clientModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [clientDb1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ReportAttachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attachments] DROP CONSTRAINT [FK_ReportAttachments];
GO
IF OBJECT_ID(N'[dbo].[FK_ReportReportCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reports] DROP CONSTRAINT [FK_ReportReportCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserUser];
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
IF OBJECT_ID(N'[dbo].[RequestModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RequestModels];
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
    [Gender] bit  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [DefaultUser] bit  NOT NULL,
    [UserId] int  NOT NULL,
    [isNew] bit  NOT NULL
);
GO

-- Creating table 'Reports'
CREATE TABLE [dbo].[Reports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sender_ID] int  NULL,
    [Sender] nvarchar(max)  NULL,
    [Recipient_ID] int  NOT NULL,
    [Recipient] nvarchar(max)  NOT NULL,
    [SendDate] datetime  NOT NULL,
    [isRead] bit  NOT NULL,
    [isMark] bit  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [ServerId] int  NULL,
    [ReportCategory_Id] int  NOT NULL
);
GO

-- Creating table 'Attachments'
CREATE TABLE [dbo].[Attachments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileLocation] nvarchar(max)  NOT NULL,
    [uploadTime] datetime  NOT NULL,
    [Report_Id] int  NOT NULL
);
GO

-- Creating table 'ReportCategories'
CREATE TABLE [dbo].[ReportCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RequestModels'
CREATE TABLE [dbo].[RequestModels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sender] nvarchar(max)  NOT NULL,
    [Recipient] nvarchar(max)  NOT NULL,
    [SendDate] datetime  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Context] nvarchar(max)  NOT NULL,
    [SenderId] int  NOT NULL,
    [RecipientId] int  NOT NULL,
    [follow] bit  NOT NULL,
    [Answer] nvarchar(max)  NULL,
    [Status] bit  NULL,
    [IsNew] nvarchar(max)  NULL,
    [IsFollowNew] bit  NULL
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

-- Creating primary key on [Id] in table 'RequestModels'
ALTER TABLE [dbo].[RequestModels]
ADD CONSTRAINT [PK_RequestModels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Report_Id] in table 'Attachments'
ALTER TABLE [dbo].[Attachments]
ADD CONSTRAINT [FK_ReportAttachments]
    FOREIGN KEY ([Report_Id])
    REFERENCES [dbo].[Reports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReportAttachments'
CREATE INDEX [IX_FK_ReportAttachments]
ON [dbo].[Attachments]
    ([Report_Id]);
GO

-- Creating foreign key on [ReportCategory_Id] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_ReportReportCategory]
    FOREIGN KEY ([ReportCategory_Id])
    REFERENCES [dbo].[ReportCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReportReportCategory'
CREATE INDEX [IX_FK_ReportReportCategory]
ON [dbo].[Reports]
    ([ReportCategory_Id]);
GO

-- Creating foreign key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser'
CREATE INDEX [IX_FK_UserUser]
ON [dbo].[Users]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------