CREATE TABLE [dbo].[TaskCategory] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Name]     NVARCHAR (250)   NOT NULL,
    [RoleName] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_TaskCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);



