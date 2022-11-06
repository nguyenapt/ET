CREATE TABLE [dbo].[SowRoleTimeStamps] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [SowRoleId]       UNIQUEIDENTIFIER NOT NULL,
    [StartDate]       DATE             NOT NULL,
    [EndDate]         DATE             NOT NULL,
    [ActualRate]      FLOAT (53)       NOT NULL,
    [EstHoursPerWeek] FLOAT (53)       NOT NULL,
    [Estimate]        FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_SowRoleTimeStamps] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SowRoleTimeStamps_SOWRole_SowRoleId] FOREIGN KEY ([SowRoleId]) REFERENCES [dbo].[SOWRole] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_SowRoleTimeStamps_SowRoleId]
    ON [dbo].[SowRoleTimeStamps]([SowRoleId] ASC);

