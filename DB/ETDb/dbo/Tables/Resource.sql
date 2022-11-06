CREATE TABLE [dbo].[Resource] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [UserId]            BIGINT           NULL,
    [FirstName]         NVARCHAR (50)    NULL,
    [LastName]          NVARCHAR (50)    NULL,
    [EmployeeCode]      NVARCHAR (50)    NULL,
    [TimeStamp]         DATETIME2 (7)    NOT NULL,
    [Country]           NVARCHAR (50)    NULL,
    [DepartmentId]      UNIQUEIDENTIFIER NULL,
    [WorkingHourRuleId] UNIQUEIDENTIFIER NULL,
    [EndDate]           DATETIME2 (7)    NULL,
    [IsKAM]             BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [StartDate]         DATETIME2 (7)    DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [UserName]          NVARCHAR (256)   NULL,
    [Skype]             NVARCHAR (256)   NULL,
    [IsActive]          BIT              NULL,
    [JobTitle]          NVARCHAR (256)   NULL,
    [JobTitleLevel]     NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Resource_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_Resource_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id]),
    CONSTRAINT [FK_Resource_WorkingHourRule_WorkingHourRuleId] FOREIGN KEY ([WorkingHourRuleId]) REFERENCES [dbo].[WorkingHourRule] ([Id])
);
















GO
CREATE NONCLUSTERED INDEX [IX_Resource_WorkingHourRuleId]
    ON [dbo].[Resource]([WorkingHourRuleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Resource_UserId]
    ON [dbo].[Resource]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Resource_DepartmentId]
    ON [dbo].[Resource]([DepartmentId] ASC);


GO


