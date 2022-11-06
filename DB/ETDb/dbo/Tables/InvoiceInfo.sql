CREATE TABLE [dbo].[InvoiceInfo] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [InvoiceName]    NVARCHAR (250)   NOT NULL,
    [InvoiceAddress] NVARCHAR (250)   NULL,
    CONSTRAINT [PK_InvoiceInfo] PRIMARY KEY CLUSTERED ([Id] ASC)
);

