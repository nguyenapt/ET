CREATE TABLE [dbo].[Client] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]         DATETIME2 (7)    DEFAULT ('2020-02-19T11:30:09.2097911+07:00') NOT NULL,
    [CreatorUserId]        BIGINT           NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [LastModifierUserId]   BIGINT           NULL,
    [IsDeleted]            BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [DeleterUserId]        BIGINT           NULL,
    [DeletionTime]         DATETIME2 (7)    NULL,
    [Name]                 NVARCHAR (250)   NOT NULL,
    [Email]                NVARCHAR (MAX)   NULL,
    [Description]          NVARCHAR (MAX)   NULL,
    [Address]              NVARCHAR (250)   NOT NULL,
    [Website]              NVARCHAR (MAX)   NULL,
    [ClientCode]           NVARCHAR (50)    NULL,
    [KAMResourceId]        UNIQUEIDENTIFIER NULL,
    [PMOId]                UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Client_Resource_KAMResourceId] FOREIGN KEY ([KAMResourceId]) REFERENCES [dbo].[Resource] ([Id]),
    CONSTRAINT [FK_Client_Resource_PMOId] FOREIGN KEY ([PMOId]) REFERENCES [dbo].[Resource] ([Id])
);




















GO
CREATE NONCLUSTERED INDEX [IX_Client_KAMResourceId]
    ON [dbo].[Client]([KAMResourceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Client_PMOId]
    ON [dbo].[Client]([PMOId] ASC);

