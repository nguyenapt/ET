CREATE TABLE [dbo].[SkillLevel] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Level]       NVARCHAR (MAX)   NULL,
    [Description] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_SkillLevel] PRIMARY KEY CLUSTERED ([Id] ASC)
);

