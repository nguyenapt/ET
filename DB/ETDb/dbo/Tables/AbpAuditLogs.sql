CREATE TABLE [dbo].[AbpAuditLogs] (
    [Id]                   BIGINT          IDENTITY (1, 1) NOT NULL,
    [TenantId]             INT             NULL,
    [UserId]               BIGINT          NULL,
    [ServiceName]          NVARCHAR (256)  NULL,
    [MethodName]           NVARCHAR (256)  NULL,
    [Parameters]           NVARCHAR (1024) NULL,
    [ReturnValue]          NVARCHAR (MAX)  NULL,
    [ExecutionTime]        DATETIME2 (7)   NOT NULL,
    [ExecutionDuration]    INT             NOT NULL,
    [ClientIpAddress]      NVARCHAR (64)   NULL,
    [ClientName]           NVARCHAR (128)  NULL,
    [BrowserInfo]          NVARCHAR (512)  NULL,
    [Exception]            NVARCHAR (2000) NULL,
    [ImpersonatorUserId]   BIGINT          NULL,
    [ImpersonatorTenantId] INT             NULL,
    [CustomData]           NVARCHAR (2000) NULL,
    CONSTRAINT [PK_AbpAuditLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpAuditLogs_TenantId_UserId]
    ON [dbo].[AbpAuditLogs]([TenantId] ASC, [UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpAuditLogs_TenantId_ExecutionTime]
    ON [dbo].[AbpAuditLogs]([TenantId] ASC, [ExecutionTime] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpAuditLogs_TenantId_ExecutionDuration]
    ON [dbo].[AbpAuditLogs]([TenantId] ASC, [ExecutionDuration] ASC);

