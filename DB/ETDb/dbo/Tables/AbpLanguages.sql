CREATE TABLE [dbo].[AbpLanguages] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [CreationTime]         DATETIME2 (7)  NOT NULL,
    [CreatorUserId]        BIGINT         NULL,
    [LastModificationTime] DATETIME2 (7)  NULL,
    [LastModifierUserId]   BIGINT         NULL,
    [IsDeleted]            BIT            NOT NULL,
    [DeleterUserId]        BIGINT         NULL,
    [DeletionTime]         DATETIME2 (7)  NULL,
    [TenantId]             INT            NULL,
    [Name]                 NVARCHAR (128) NOT NULL,
    [DisplayName]          NVARCHAR (64)  NOT NULL,
    [Icon]                 NVARCHAR (128) NULL,
    [IsDisabled]           BIT            NOT NULL,
    CONSTRAINT [PK_AbpLanguages] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpLanguages_TenantId_Name]
    ON [dbo].[AbpLanguages]([TenantId] ASC, [Name] ASC);

