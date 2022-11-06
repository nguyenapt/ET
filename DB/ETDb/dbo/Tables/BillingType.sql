CREATE TABLE [dbo].[BillingType] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NULL,
    [Value] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_BillingType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

