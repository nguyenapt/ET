﻿CREATE TABLE [dbo].[AbpRoles] (
    [Id]                   INT              IDENTITY (1, 1) NOT NULL,
    [CreationTime]         DATETIME2 (7)    NOT NULL,
    [CreatorUserId]        BIGINT           NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [LastModifierUserId]   BIGINT           NULL,
    [IsDeleted]            BIT              NOT NULL,
    [DeleterUserId]        BIGINT           NULL,
    [DeletionTime]         DATETIME2 (7)    NULL,
    [TenantId]             INT              NULL,
    [Name]                 NVARCHAR (32)    NOT NULL,
    [DisplayName]          NVARCHAR (64)    NOT NULL,
    [IsStatic]             BIT              NOT NULL,
    [IsDefault]            BIT              NOT NULL,
    [NormalizedName]       NVARCHAR (32)    NOT NULL,
    [ConcurrencyStamp]     NVARCHAR (128)   NULL,
    [Description]          NVARCHAR (MAX)   NULL,
    [TaskCategoryId]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_AbpRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AbpRoles_AbpUsers_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpRoles_AbpUsers_DeleterUserId] FOREIGN KEY ([DeleterUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpRoles_AbpUsers_LastModifierUserId] FOREIGN KEY ([LastModifierUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpRoles_TaskCategory_TaskCategoryId] FOREIGN KEY ([TaskCategoryId]) REFERENCES [dbo].[TaskCategory] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpRoles_TenantId_NormalizedName]
    ON [dbo].[AbpRoles]([TenantId] ASC, [NormalizedName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpRoles_TaskCategoryId]
    ON [dbo].[AbpRoles]([TaskCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpRoles_LastModifierUserId]
    ON [dbo].[AbpRoles]([LastModifierUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpRoles_DeleterUserId]
    ON [dbo].[AbpRoles]([DeleterUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpRoles_CreatorUserId]
    ON [dbo].[AbpRoles]([CreatorUserId] ASC);

