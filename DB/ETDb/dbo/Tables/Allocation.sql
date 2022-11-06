CREATE TABLE [dbo].[Allocation] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]         DATETIME2 (7)    NOT NULL,
    [CreatorUserId]        BIGINT           NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [LastModifierUserId]   BIGINT           NULL,
    [IsDeleted]            BIT              NOT NULL,
    [DeleterUserId]        BIGINT           NULL,
    [DeletionTime]         DATETIME2 (7)    NULL,
    [IsBillable]           BIT              NOT NULL,
    [SOWRoleId]            UNIQUEIDENTIFIER NOT NULL,
    [ResourceId]           UNIQUEIDENTIFIER NOT NULL,
    [RateType]             NVARCHAR (10)    NOT NULL,
    [FTE]                  FLOAT (53)       NULL,
    [TotalHours]           FLOAT (53)       NULL,
    [TotalHoursPerMonth]   FLOAT (53)       NULL,
    [StartDate]            DATE             NOT NULL,
    [EndDate]              DATE             NULL,
    [Description]          NVARCHAR (1000)  NULL,
    [IsActive]             BIT              NOT NULL,
    [TimeNote]             NVARCHAR (MAX)   NULL,
    [EstHoursPerWeek]      FLOAT (53)       NULL,
    [ForecastTime]         DATE             NULL,
    [AllocationTypeId]     INT              NULL,
    [AllocationStatusId]   INT              NULL,
    CONSTRAINT [PK_Allocation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Allocation_AllocationType_AllocationTypeId] FOREIGN KEY ([AllocationTypeId]) REFERENCES [dbo].[AllocationType] ([Id]),
    CONSTRAINT [FK_Allocation_Resource_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Allocation_SOWRole_SOWRoleId] FOREIGN KEY ([SOWRoleId]) REFERENCES [dbo].[SOWRole] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AllocationStatus_Allocation] FOREIGN KEY ([AllocationStatusId]) REFERENCES [dbo].[AllocationStatus] ([ID])
);

























GO
CREATE NONCLUSTERED INDEX [IX_Allocation_SOWRoleId]
    ON [dbo].[Allocation]([SOWRoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Allocation_ResourceId]
    ON [dbo].[Allocation]([ResourceId] ASC);

