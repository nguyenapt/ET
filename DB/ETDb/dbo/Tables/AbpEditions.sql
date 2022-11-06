CREATE TABLE [dbo].[AbpEditions] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [CreationTime]         DATETIME2 (7) NOT NULL,
    [CreatorUserId]        BIGINT        NULL,
    [LastModificationTime] DATETIME2 (7) NULL,
    [LastModifierUserId]   BIGINT        NULL,
    [IsDeleted]            BIT           NOT NULL,
    [DeleterUserId]        BIGINT        NULL,
    [DeletionTime]         DATETIME2 (7) NULL,
    [Name]                 NVARCHAR (32) NOT NULL,
    [DisplayName]          NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_AbpEditions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

