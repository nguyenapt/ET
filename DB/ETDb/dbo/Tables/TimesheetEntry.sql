CREATE TABLE [dbo].[TimesheetEntry] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [TaskId]             UNIQUEIDENTIFIER NULL,
    [AllocationId]       UNIQUEIDENTIFIER NULL,
    [LeavePermissionId]  UNIQUEIDENTIFIER NULL,
    [Description]        NVARCHAR (250)   NULL,
    [TicketName]         NVARCHAR (20)    NULL,
    [Hours]              DECIMAL (18, 2)  NULL,
    [RecordDate]         DATE             NOT NULL,
    [SubmittedTimestamp] DATETIME2 (7)    NULL,
    [ApprovedTimestamp]  DATETIME2 (7)    NULL,
    [isOverTime]         BIT              NULL,
    [ApprovalStatus]     TINYINT          NULL,
    [ApprovalId]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_TimesheetEntry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TimesheetEntry_Allocation_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [dbo].[Allocation] ([Id]),
    CONSTRAINT [FK_TimesheetEntry_LeavePermission_LeavePermissionId] FOREIGN KEY ([LeavePermissionId]) REFERENCES [dbo].[LeavePermission] ([Id]),
    CONSTRAINT [FK_TimesheetEntry_Resource_ApprovalId] FOREIGN KEY ([ApprovalId]) REFERENCES [dbo].[Resource] ([Id]),
    CONSTRAINT [FK_TimesheetEntry_Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([Id])
);








GO
CREATE NONCLUSTERED INDEX [IX_TimesheetEntry_TaskId]
    ON [dbo].[TimesheetEntry]([TaskId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TimesheetEntry_LeavePermissionId]
    ON [dbo].[TimesheetEntry]([LeavePermissionId] ASC);


GO



GO
CREATE NONCLUSTERED INDEX [IX_TimesheetEntry_ApprovalId]
    ON [dbo].[TimesheetEntry]([ApprovalId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TimesheetEntry_AllocationId]
    ON [dbo].[TimesheetEntry]([AllocationId] ASC);

