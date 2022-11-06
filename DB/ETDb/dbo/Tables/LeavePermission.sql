CREATE TABLE [dbo].[LeavePermission] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [ResourceId]           UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    [StartDate]            DATETIME2 (7)    NOT NULL,
    [EndDate]              DATETIME2 (7)    NOT NULL,
    [TotalHours]           DECIMAL (18, 2)  NOT NULL,
    [LeaveTypeId]          UNIQUEIDENTIFIER NOT NULL,
    [IsFullDay]            BIT              NOT NULL,
    [ApprovalStatus]       TINYINT          NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [CreationTime]         DATETIME2 (7)    NOT NULL,
    [Reason]               NVARCHAR (MAX)   NULL,
    [RejectReason]         NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_LeavePermission] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LeavePermission_LeaveType_LeaveTypeId] FOREIGN KEY ([LeaveTypeId]) REFERENCES [dbo].[LeaveType] ([Id]),
    CONSTRAINT [FK_LeavePermission_Resource_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([Id]) ON DELETE CASCADE
);










GO
CREATE NONCLUSTERED INDEX [IX_LeavePermission_ResourceId]
    ON [dbo].[LeavePermission]([ResourceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LeavePermission_LeaveTypeId]
    ON [dbo].[LeavePermission]([LeaveTypeId] ASC);

