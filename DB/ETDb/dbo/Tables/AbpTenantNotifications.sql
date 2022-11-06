﻿CREATE TABLE [dbo].[AbpTenantNotifications] (
    [Id]                              UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]                    DATETIME2 (7)    NOT NULL,
    [CreatorUserId]                   BIGINT           NULL,
    [TenantId]                        INT              NULL,
    [NotificationName]                NVARCHAR (96)    NOT NULL,
    [Data]                            NVARCHAR (MAX)   NULL,
    [DataTypeName]                    NVARCHAR (512)   NULL,
    [EntityTypeName]                  NVARCHAR (250)   NULL,
    [EntityTypeAssemblyQualifiedName] NVARCHAR (512)   NULL,
    [EntityId]                        NVARCHAR (96)    NULL,
    [Severity]                        TINYINT          NOT NULL,
    CONSTRAINT [PK_AbpTenantNotifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpTenantNotifications_TenantId]
    ON [dbo].[AbpTenantNotifications]([TenantId] ASC);

