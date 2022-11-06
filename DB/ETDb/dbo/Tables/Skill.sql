CREATE TABLE [dbo].[Skill] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED ([Id] ASC)
);





