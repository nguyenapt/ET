CREATE TABLE [dbo].[ProjectType] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NULL,
    [Scope] NVARCHAR (50) NULL,
    [P/L]   BIT           NOT NULL,
    CONSTRAINT [PK_ProjectType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

