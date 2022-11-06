CREATE TABLE [dbo].[Project] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]         DATETIME2 (7)    DEFAULT ('2020-03-02T14:14:10.5985163+07:00') NOT NULL,
    [CreatorUserId]        BIGINT           NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [LastModifierUserId]   BIGINT           NULL,
    [IsDeleted]            BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [DeleterUserId]        BIGINT           NULL,
    [DeletionTime]         DATETIME2 (7)    NULL,
    [ClientId]             UNIQUEIDENTIFIER NULL,
    [Name]                 NVARCHAR (250)   NOT NULL,
    [StartDate]            DATE             NULL,
    [EndDate]              DATE             NULL,
    [DepartmentId]         UNIQUEIDENTIFIER NULL,
    [UniqueCode]           INT              IDENTITY (1, 1) NOT NULL,
    [ProjectTag]           NVARCHAR (65)    NULL,
    [ProjectCode]          NVARCHAR (MAX)   NULL,
    [Description]          NVARCHAR (MAX)   NULL,
    [ProjectManagerId]     UNIQUEIDENTIFIER NULL,
    [PMOId]                UNIQUEIDENTIFIER NULL,
    [ProjectTypeId]        INT              NULL,
    [ProjectStateId]       INT              NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Project_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]),
    CONSTRAINT [FK_Project_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id]),
    CONSTRAINT [FK_Project_ProjectStateType_ProjectStateId] FOREIGN KEY ([ProjectStateId]) REFERENCES [dbo].[ProjectStateType] ([Id]),
    CONSTRAINT [FK_Project_ProjectType_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectType] ([Id]),
    CONSTRAINT [FK_Project_Resource_PMOId] FOREIGN KEY ([PMOId]) REFERENCES [dbo].[Resource] ([Id]),
    CONSTRAINT [FK_Project_Resource_ProjectManagerId] FOREIGN KEY ([ProjectManagerId]) REFERENCES [dbo].[Resource] ([Id])
);


























GO
CREATE NONCLUSTERED INDEX [IX_Project_DepartmentId]
    ON [dbo].[Project]([DepartmentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_ClientId]
    ON [dbo].[Project]([ClientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_ProjectManagerId]
    ON [dbo].[Project]([ProjectManagerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_ProjectTypeId]
    ON [dbo].[Project]([ProjectTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_PMOId]
    ON [dbo].[Project]([PMOId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_ProjectStateId]
    ON [dbo].[Project]([ProjectStateId] ASC);

