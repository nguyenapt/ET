CREATE TABLE [dbo].[BillingRate] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ResourceRole]  NVARCHAR (MAX) NULL,
    [Currency]      NVARCHAR (MAX) NULL,
    [MonthlyRate]   NVARCHAR (MAX) NULL,
    [DailyRate]     NVARCHAR (MAX) NULL,
    [HourlyRate]    NVARCHAR (MAX) NULL,
    [EffectiveDate] DATE           NOT NULL,
    CONSTRAINT [PK_BillingRate] PRIMARY KEY CLUSTERED ([Id] ASC)
);



