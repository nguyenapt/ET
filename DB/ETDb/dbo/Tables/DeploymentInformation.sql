CREATE TABLE [dbo].[DeploymentInformation] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [UpdateDate]      DATETIME         NOT NULL,
    [VersionNumber]   NVARCHAR (MAX)   NULL,
    [EnvironmentCode] NVARCHAR (MAX)   NULL,
    [ProjectCode]     NVARCHAR (MAX)   NULL,
    [Description]     NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_DeploymentInformation] PRIMARY KEY CLUSTERED ([Id] ASC)
);



