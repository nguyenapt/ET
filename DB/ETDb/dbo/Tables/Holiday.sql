CREATE TABLE [dbo].[Holiday] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Country]     NVARCHAR (MAX)   NULL,
    [HolidayDate] DATE             NOT NULL,
    [HolidayName] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED ([Id] ASC)
);



