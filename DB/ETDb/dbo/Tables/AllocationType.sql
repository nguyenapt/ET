CREATE TABLE [dbo].[AllocationType] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Code]        NVARCHAR (50) NULL,
    [IsSupporter] BIT           NULL,
    CONSTRAINT [PK_AllocationType_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);





