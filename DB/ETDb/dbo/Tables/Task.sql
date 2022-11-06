CREATE TABLE [dbo].[Task] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (50)    NULL,
    [TaskCategoryId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Task_TaskCategory_TaskCategoryId] FOREIGN KEY ([TaskCategoryId]) REFERENCES [dbo].[TaskCategory] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Task_TaskCategoryId]
    ON [dbo].[Task]([TaskCategoryId] ASC);

