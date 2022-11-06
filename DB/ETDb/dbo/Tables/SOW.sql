CREATE TABLE [dbo].[SOW] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [CreationTime]         DATETIME2 (7)    NOT NULL,
    [CreatorUserId]        BIGINT           NULL,
    [LastModificationTime] DATETIME2 (7)    NULL,
    [LastModifierUserId]   BIGINT           NULL,
    [IsDeleted]            BIT              NOT NULL,
    [DeleterUserId]        BIGINT           NULL,
    [DeletionTime]         DATETIME2 (7)    NULL,
    [ProjectId]            UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    [Name]                 NVARCHAR (250)   NOT NULL,
    [FileUrl]              NVARCHAR (MAX)   NULL,
    [StartDate]            DATE             NULL,
    [EndDate]              DATE             NULL,
    [Status]               NVARCHAR (MAX)   NULL,
    [ClientPONumber]       NVARCHAR (50)    NULL,
    [Description]          NVARCHAR (MAX)   NULL,
    [SowNumber]            INT              NULL,
    [Version]              DECIMAL (18, 2)  NULL,
    [InvoicingCycle]       NVARCHAR (50)    NULL,
    [1stInvoiceDate]       DATE             NULL,
    [BeneficiaryId]        UNIQUEIDENTIFIER NULL,
    [InvoiceInfoId]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SOW] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SOW_Beneficiary_BeneficiaryId] FOREIGN KEY ([BeneficiaryId]) REFERENCES [dbo].[Beneficiary] ([Id]),
    CONSTRAINT [FK_SOW_InvoiceInfo_InvoiceInfoId] FOREIGN KEY ([InvoiceInfoId]) REFERENCES [dbo].[InvoiceInfo] ([Id]),
    CONSTRAINT [FK_SOW_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);
























GO
CREATE NONCLUSTERED INDEX [IX_SOW_ProjectId]
    ON [dbo].[SOW]([ProjectId] ASC);


GO



GO
CREATE NONCLUSTERED INDEX [IX_SOW_InvoiceInfoId]
    ON [dbo].[SOW]([InvoiceInfoId] ASC);


GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_SOW_BeneficiaryId]
    ON [dbo].[SOW]([BeneficiaryId] ASC);

