CREATE TABLE [dbo].[RateType] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NULL,
    [Value] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_RateType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

