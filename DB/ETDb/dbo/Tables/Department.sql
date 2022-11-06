CREATE TABLE [dbo].[Department] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (50)    NOT NULL,
    [DepartmentCode] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([Id] ASC)
);







