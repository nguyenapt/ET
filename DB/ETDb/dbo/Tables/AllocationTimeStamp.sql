CREATE TABLE [dbo].[AllocationTimeStamp] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [AllocationId]    UNIQUEIDENTIFIER NOT NULL,
    [StartDate]       DATE             NOT NULL,
    [EndDate]         DATE             NOT NULL,
    [ActualRate]      FLOAT (53)       NOT NULL,
    [EstHoursPerWeek] FLOAT (53)       NOT NULL,
    [Estimate]        FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_AllocationTimeStamp] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AllocationTimeStamp_Allocation_AllocationId] FOREIGN KEY ([AllocationId]) REFERENCES [dbo].[Allocation] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_AllocationTimeStamp_AllocationId]
    ON [dbo].[AllocationTimeStamp]([AllocationId] ASC);

