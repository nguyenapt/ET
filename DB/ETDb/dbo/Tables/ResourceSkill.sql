CREATE TABLE [dbo].[ResourceSkill] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [ResourceId]   UNIQUEIDENTIFIER NOT NULL,
    [SkillId]      UNIQUEIDENTIFIER NOT NULL,
    [SkillLevelId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ResourceSkill] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResourceSkill_Resource_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ResourceSkill_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Skill] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ResourceSkill_SkillLevel_SkillLevelId] FOREIGN KEY ([SkillLevelId]) REFERENCES [dbo].[SkillLevel] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_ResourceSkill_SkillId]
    ON [dbo].[ResourceSkill]([SkillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ResourceSkill_ResourceId]
    ON [dbo].[ResourceSkill]([ResourceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ResourceSkill_SkillLevelId]
    ON [dbo].[ResourceSkill]([SkillLevelId] ASC);

