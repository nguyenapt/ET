CREATE TABLE [dbo].[AbpUsers] (
    [Id]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreationTime]           DATETIME2 (7)  NOT NULL,
    [CreatorUserId]          BIGINT         NULL,
    [LastModificationTime]   DATETIME2 (7)  NULL,
    [LastModifierUserId]     BIGINT         NULL,
    [IsDeleted]              BIT            NOT NULL,
    [DeleterUserId]          BIGINT         NULL,
    [DeletionTime]           DATETIME2 (7)  NULL,
    [AuthenticationSource]   NVARCHAR (64)  NULL,
    [UserName]               NVARCHAR (256) NOT NULL,
    [TenantId]               INT            NULL,
    [EmailAddress]           NVARCHAR (256) NOT NULL,
    [Name]                   NVARCHAR (64)  NOT NULL,
    [Surname]                NVARCHAR (64)  NOT NULL,
    [Password]               NVARCHAR (128) NOT NULL,
    [EmailConfirmationCode]  NVARCHAR (328) NULL,
    [PasswordResetCode]      NVARCHAR (328) NULL,
    [LockoutEndDateUtc]      DATETIME2 (7)  NULL,
    [AccessFailedCount]      INT            NOT NULL,
    [IsLockoutEnabled]       BIT            NOT NULL,
    [PhoneNumber]            NVARCHAR (32)  NULL,
    [IsPhoneNumberConfirmed] BIT            NOT NULL,
    [SecurityStamp]          NVARCHAR (128) NULL,
    [IsTwoFactorEnabled]     BIT            NOT NULL,
    [IsEmailConfirmed]       BIT            NOT NULL,
    [IsActive]               BIT            NOT NULL,
    [NormalizedUserName]     NVARCHAR (256) NOT NULL,
    [NormalizedEmailAddress] NVARCHAR (256) NOT NULL,
    [ConcurrencyStamp]       NVARCHAR (128) NULL,
    CONSTRAINT [PK_AbpUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AbpUsers_AbpUsers_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpUsers_AbpUsers_DeleterUserId] FOREIGN KEY ([DeleterUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpUsers_AbpUsers_LastModifierUserId] FOREIGN KEY ([LastModifierUserId]) REFERENCES [dbo].[AbpUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_TenantId_NormalizedUserName]
    ON [dbo].[AbpUsers]([TenantId] ASC, [NormalizedUserName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_TenantId_NormalizedEmailAddress]
    ON [dbo].[AbpUsers]([TenantId] ASC, [NormalizedEmailAddress] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_LastModifierUserId]
    ON [dbo].[AbpUsers]([LastModifierUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_DeleterUserId]
    ON [dbo].[AbpUsers]([DeleterUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_CreatorUserId]
    ON [dbo].[AbpUsers]([CreatorUserId] ASC);

