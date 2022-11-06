CREATE TABLE [dbo].[AbpNotifications] (
    [Id]                              UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]                    DATETIME2 (7)    NOT NULL,
    [CreatorUserId]                   BIGINT           NULL,
    [NotificationName]                NVARCHAR (96)    NOT NULL,
    [Data]                            NVARCHAR (MAX)   NULL,
    [DataTypeName]                    NVARCHAR (512)   NULL,
    [EntityTypeName]                  NVARCHAR (250)   NULL,
    [EntityTypeAssemblyQualifiedName] NVARCHAR (512)   NULL,
    [EntityId]                        NVARCHAR (96)    NULL,
    [Severity]                        TINYINT          NOT NULL,
    [UserIds]                         NVARCHAR (MAX)   NULL,
    [ExcludedUserIds]                 NVARCHAR (MAX)   NULL,
    [TenantIds]                       NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AbpNotifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);

