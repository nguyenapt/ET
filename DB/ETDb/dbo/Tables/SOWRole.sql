CREATE TABLE [dbo].[SOWRole] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]         DATETIME2 (7)    NOT NULL,
    [CreatorUserId]        BIGINT           NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [LastModifierUserId]   BIGINT           NULL,
    [IsDeleted]            BIT              NOT NULL,
    [DeleterUserId]        BIGINT           NULL,
    [DeletionTime]         DATETIME2 (7)    NULL,
    [SOWId]                UNIQUEIDENTIFIER NOT NULL,
    [IsBillable]           BIT              NOT NULL,
    [BillingType]          NVARCHAR (50)    NOT NULL,
    [RoleName]             NVARCHAR (50)    DEFAULT (N'') NOT NULL,
    [RateType]             NVARCHAR (10)    NOT NULL,
    [Currency]             NVARCHAR (3)     NOT NULL,
    [StandardRate]         FLOAT (53)       NOT NULL,
    [ActualRate]           FLOAT (53)       NOT NULL,
    [FTE]                  FLOAT (53)       NULL,
    [TotalHours]           FLOAT (53)       NULL,
    [TotalHoursPerMonth]   FLOAT (53)       NULL,
    [StartDate]            DATE             NOT NULL,
    [EndDate]              DATE             NULL,
    [Term]                 INT              NULL,
    [Description]          NVARCHAR (MAX)   NULL,
    [EstHoursPerWeek]      FLOAT (53)       NULL,
    [ForecastTime]         DATE             NULL,
    [InternalTypeId]       UNIQUEIDENTIFIER NULL,
    [TimeNote]             NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_SOWRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SOWRole_InternalType_InternalTypeId] FOREIGN KEY ([InternalTypeId]) REFERENCES [dbo].[InternalType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SOWRole_SOW_SOWId] FOREIGN KEY ([SOWId]) REFERENCES [dbo].[SOW] ([Id]) ON DELETE CASCADE
);




















GO
CREATE NONCLUSTERED INDEX [IX_SOWRole_SOWId]
    ON [dbo].[SOWRole]([SOWId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SOWRole_InternalTypeId]
    ON [dbo].[SOWRole]([InternalTypeId] ASC);

