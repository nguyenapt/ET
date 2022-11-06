﻿CREATE TABLE [dbo].[AbpTenants] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [CreationTime]         DATETIME2 (7)   NOT NULL,
    [CreatorUserId]        BIGINT          NULL,
    [LastModificationTime] DATETIME2 (7)   NULL,
    [LastModifierUserId]   BIGINT          NULL,
    [IsDeleted]            BIT             NOT NULL,
    [DeleterUserId]        BIGINT          NULL,
    [DeletionTime]         DATETIME2 (7)   NULL,
    [TenancyName]          NVARCHAR (64)   NOT NULL,
    [Name]                 NVARCHAR (128)  NOT NULL,
    [ConnectionString]     NVARCHAR (1024) NULL,
    [IsActive]             BIT             NOT NULL,
    [EditionId]            INT             NULL,
    CONSTRAINT [PK_AbpTenants] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AbpTenants_AbpEditions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[AbpEditions] ([Id]),
    CONSTRAINT [FK_AbpTenants_AbpUsers_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpTenants_AbpUsers_DeleterUserId] FOREIGN KEY ([DeleterUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpTenants_AbpUsers_LastModifierUserId] FOREIGN KEY ([LastModifierUserId]) REFERENCES [dbo].[AbpUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpTenants_TenancyName]
    ON [dbo].[AbpTenants]([TenancyName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpTenants_LastModifierUserId]
    ON [dbo].[AbpTenants]([LastModifierUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpTenants_EditionId]
    ON [dbo].[AbpTenants]([EditionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpTenants_DeleterUserId]
    ON [dbo].[AbpTenants]([DeleterUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpTenants_CreatorUserId]
    ON [dbo].[AbpTenants]([CreatorUserId] ASC);

