﻿CREATE TABLE [dbo].[AbpOrganizationUnits] (
    [Id]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreationTime]         DATETIME2 (7)  NOT NULL,
    [CreatorUserId]        BIGINT         NULL,
    [LastModificationTime] DATETIME2 (7)  NULL,
    [LastModifierUserId]   BIGINT         NULL,
    [IsDeleted]            BIT            NOT NULL,
    [DeleterUserId]        BIGINT         NULL,
    [DeletionTime]         DATETIME2 (7)  NULL,
    [TenantId]             INT            NULL,
    [ParentId]             BIGINT         NULL,
    [Code]                 NVARCHAR (95)  NOT NULL,
    [DisplayName]          NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_AbpOrganizationUnits] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[AbpOrganizationUnits] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpOrganizationUnits_TenantId_Code]
    ON [dbo].[AbpOrganizationUnits]([TenantId] ASC, [Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpOrganizationUnits_ParentId]
    ON [dbo].[AbpOrganizationUnits]([ParentId] ASC);

