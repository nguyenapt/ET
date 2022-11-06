CREATE TABLE [dbo].[AbpLanguageTexts] (
    [Id]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreationTime]         DATETIME2 (7)  NOT NULL,
    [CreatorUserId]        BIGINT         NULL,
    [LastModificationTime] DATETIME2 (7)  NULL,
    [LastModifierUserId]   BIGINT         NULL,
    [TenantId]             INT            NULL,
    [LanguageName]         NVARCHAR (128) NOT NULL,
    [Source]               NVARCHAR (128) NOT NULL,
    [Key]                  NVARCHAR (256) NOT NULL,
    [Value]                NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_AbpLanguageTexts] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpLanguageTexts_TenantId_Source_LanguageName_Key]
    ON [dbo].[AbpLanguageTexts]([TenantId] ASC, [Source] ASC, [LanguageName] ASC, [Key] ASC);

