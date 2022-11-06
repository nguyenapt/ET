CREATE TABLE [dbo].[Beneficiary] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [Name]   NVARCHAR (250)   NOT NULL,
    [Detail] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Beneficiary] PRIMARY KEY CLUSTERED ([Id] ASC)
);



