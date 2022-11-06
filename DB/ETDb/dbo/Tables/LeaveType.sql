CREATE TABLE [dbo].[LeaveType] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (250)   NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_LeaveType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

