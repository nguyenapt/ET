CREATE TABLE [dbo].[AbpNotificationSubscriptions] (
    [Id]                              UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]                    DATETIME2 (7)    NOT NULL,
    [CreatorUserId]                   BIGINT           NULL,
    [TenantId]                        INT              NULL,
    [UserId]                          BIGINT           NOT NULL,
    [NotificationName]                NVARCHAR (96)    NULL,
    [EntityTypeName]                  NVARCHAR (250)   NULL,
    [EntityTypeAssemblyQualifiedName] NVARCHAR (512)   NULL,
    [EntityId]                        NVARCHAR (96)    NULL,
    CONSTRAINT [PK_AbpNotificationSubscriptions] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpNotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId]
    ON [dbo].[AbpNotificationSubscriptions]([TenantId] ASC, [NotificationName] ASC, [EntityTypeName] ASC, [EntityId] ASC, [UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpNotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId]
    ON [dbo].[AbpNotificationSubscriptions]([NotificationName] ASC, [EntityTypeName] ASC, [EntityId] ASC, [UserId] ASC);

