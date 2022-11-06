CREATE TABLE [dbo].[ProjectStateType] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [State] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ProjectStateType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

