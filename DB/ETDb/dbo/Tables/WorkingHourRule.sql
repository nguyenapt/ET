CREATE TABLE [dbo].[WorkingHourRule] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [Name]                   NVARCHAR (50)    NULL,
    [RequiredMondayHours]    REAL             NULL,
    [RequiredTuesdayHours]   REAL             NULL,
    [RequiredWednesdayHours] REAL             NULL,
    [RequiredThursdayHours]  REAL             NULL,
    [RequiredFridayHours]    REAL             NULL,
    [RequiredSaturdayHours]  REAL             NULL,
    [RequiredSundayHours]    REAL             NULL,
    CONSTRAINT [PK_WorkingHourRule] PRIMARY KEY CLUSTERED ([Id] ASC)
);



