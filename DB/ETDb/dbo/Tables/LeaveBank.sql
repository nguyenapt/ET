CREATE TABLE [dbo].[LeaveBank] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ResourceId]        UNIQUEIDENTIFIER NOT NULL,
    [TotalAllowedHours] DECIMAL (18, 2)  NOT NULL,
    [LeaveTypeId]       UNIQUEIDENTIFIER NOT NULL,
    [Year]              INT              NOT NULL,
    CONSTRAINT [PK_LeaveBank] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LeaveBank_LeaveType_LeaveTypeId] FOREIGN KEY ([LeaveTypeId]) REFERENCES [dbo].[LeaveType] ([Id]),
    CONSTRAINT [FK_LeaveBank_Resource_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([Id]) ON DELETE CASCADE
);

