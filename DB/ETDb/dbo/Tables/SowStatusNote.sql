CREATE TABLE [dbo].[SowStatusNote] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [Status]               NVARCHAR (450)   NOT NULL,
    [CreationTime]         DATETIME2 (7)    NOT NULL,
    [CreatorUserId]        BIGINT           NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [LastModifierUserId]   BIGINT           NULL,
    [SowId]                UNIQUEIDENTIFIER NOT NULL,
    [StatusNote]           NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_SowStatusNote] PRIMARY KEY CLUSTERED ([Id] ASC, [Status] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_SowStatusNote_SowId]
    ON [dbo].[SowStatusNote]([SowId] ASC);

