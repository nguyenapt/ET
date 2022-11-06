CREATE TABLE [dbo].[AbpUserClaims] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreationTime]  DATETIME2 (7)  NOT NULL,
    [CreatorUserId] BIGINT         NULL,
    [TenantId]      INT            NULL,
    [UserId]        BIGINT         NOT NULL,
    [ClaimType]     NVARCHAR (256) NULL,
    [ClaimValue]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AbpUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AbpUserClaims_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUserClaims_TenantId_ClaimType]
    ON [dbo].[AbpUserClaims]([TenantId] ASC, [ClaimType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUserClaims_UserId]
    ON [dbo].[AbpUserClaims]([UserId] ASC);

