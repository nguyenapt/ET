CREATE TABLE [dbo].[ResourceRole] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NULL,
    [Value] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ResourceRole] PRIMARY KEY CLUSTERED ([Id] ASC)
);

