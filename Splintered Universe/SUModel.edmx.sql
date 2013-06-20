
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/10/2013 14:19:59
-- Generated from EDMX file: C:\SpringGage\Splintered Universe\Splintered Universe\SUModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SU];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CampaignCharacter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Characters] DROP CONSTRAINT [FK_CampaignCharacter];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemRulesCampaign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Campaigns] DROP CONSTRAINT [FK_SystemRulesCampaign];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCharacter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Characters] DROP CONSTRAINT [FK_UserCharacter];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCampaign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Campaigns] DROP CONSTRAINT [FK_UserCampaign];
GO
IF OBJECT_ID(N'[dbo].[FK_CampaignCombatEncounter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CombatEncounters] DROP CONSTRAINT [FK_CampaignCombatEncounter];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemActionCombatEncounterItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CombatEncounterItems] DROP CONSTRAINT [FK_SystemActionCombatEncounterItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CombatEncounterCombatEncounterItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CombatEncounterItems] DROP CONSTRAINT [FK_CombatEncounterCombatEncounterItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CharacterCombatEncounterItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CombatEncounterItems] DROP CONSTRAINT [FK_CharacterCombatEncounterItem];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemRuleSystemSkillCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemSkillCategories] DROP CONSTRAINT [FK_SystemRuleSystemSkillCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemRuleSystemStat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemStats] DROP CONSTRAINT [FK_SystemRuleSystemStat];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemRuleSystemActionCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemActionCategories] DROP CONSTRAINT [FK_SystemRuleSystemActionCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemActionCategorySystemAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemActions] DROP CONSTRAINT [FK_SystemActionCategorySystemAction];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemStatSystemSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemSkills] DROP CONSTRAINT [FK_SystemStatSystemSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemSkillCategorySystemSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemSkills] DROP CONSTRAINT [FK_SystemSkillCategorySystemSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemStatSystemStatLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemStatLevels] DROP CONSTRAINT [FK_SystemStatSystemStatLevel];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemActionSystemSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SystemSkills] DROP CONSTRAINT [FK_SystemActionSystemSkill];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Characters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Characters];
GO
IF OBJECT_ID(N'[dbo].[Campaigns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Campaigns];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[SystemRules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemRules];
GO
IF OBJECT_ID(N'[dbo].[SystemSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemSkills];
GO
IF OBJECT_ID(N'[dbo].[ELMAH_Error]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ELMAH_Error];
GO
IF OBJECT_ID(N'[dbo].[SystemStatLevels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemStatLevels];
GO
IF OBJECT_ID(N'[dbo].[SystemStats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemStats];
GO
IF OBJECT_ID(N'[dbo].[SystemSkillCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemSkillCategories];
GO
IF OBJECT_ID(N'[dbo].[SystemActions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemActions];
GO
IF OBJECT_ID(N'[dbo].[SystemActionCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemActionCategories];
GO
IF OBJECT_ID(N'[dbo].[CombatEncounters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CombatEncounters];
GO
IF OBJECT_ID(N'[dbo].[CombatEncounterItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CombatEncounterItems];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Characters'
CREATE TABLE [dbo].[Characters] (
    [CharacterId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CampaignCampaignId] int  NOT NULL,
    [UserUserId] int  NOT NULL
);
GO

-- Creating table 'Campaigns'
CREATE TABLE [dbo].[Campaigns] (
    [CampaignId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [SystemRuleSystemRuleId] int  NOT NULL,
    [UserUserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [LoginName] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [CanBeGM] bit  NOT NULL,
    [IsAdmin] bit  NOT NULL
);
GO

-- Creating table 'SystemRules'
CREATE TABLE [dbo].[SystemRules] (
    [SystemRuleId] int IDENTITY(1,1) NOT NULL,
    [SystemName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SystemSkills'
CREATE TABLE [dbo].[SystemSkills] (
    [SystemSkillId] int IDENTITY(1,1) NOT NULL,
    [SystemSkillCategorySystemSkillCategoryId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Difficulty] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [GMNotes] nvarchar(max)  NOT NULL,
    [SystemStatSystemStatId] int  NOT NULL,
    [SystemActionSystemActionId] int  NOT NULL
);
GO

-- Creating table 'ELMAH_Error'
CREATE TABLE [dbo].[ELMAH_Error] (
    [ErrorId] uniqueidentifier  NOT NULL,
    [Application] nvarchar(60)  NOT NULL,
    [Host] nvarchar(50)  NOT NULL,
    [Type] nvarchar(100)  NOT NULL,
    [Source] nvarchar(60)  NOT NULL,
    [Message] nvarchar(500)  NOT NULL,
    [User] nvarchar(50)  NOT NULL,
    [StatusCode] int  NOT NULL,
    [TimeUtc] datetime  NOT NULL,
    [Sequence] int IDENTITY(1,1) NOT NULL,
    [AllXml] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SystemStatLevels'
CREATE TABLE [dbo].[SystemStatLevels] (
    [SystemStatLevelId] int IDENTITY(1,1) NOT NULL,
    [Level] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [SystemStatSystemStatId] int  NOT NULL
);
GO

-- Creating table 'SystemStats'
CREATE TABLE [dbo].[SystemStats] (
    [SystemStatId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Abbreviation] nvarchar(max)  NOT NULL,
    [SystemRuleSystemRuleId] int  NOT NULL
);
GO

-- Creating table 'SystemSkillCategories'
CREATE TABLE [dbo].[SystemSkillCategories] (
    [SystemSkillCategoryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SystemRuleSystemRuleId] int  NOT NULL
);
GO

-- Creating table 'SystemActions'
CREATE TABLE [dbo].[SystemActions] (
    [SystemActionId] int IDENTITY(1,1) NOT NULL,
    [BaseAPCost] int  NOT NULL,
    [MinAPCost] int  NOT NULL,
    [FatigueCost] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [GMNotes] nvarchar(max)  NOT NULL,
    [SystemActionCategorySystemActionCategoryId] int  NOT NULL
);
GO

-- Creating table 'SystemActionCategories'
CREATE TABLE [dbo].[SystemActionCategories] (
    [SystemActionCategoryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SystemRuleSystemRuleId] int  NOT NULL
);
GO

-- Creating table 'CombatEncounters'
CREATE TABLE [dbo].[CombatEncounters] (
    [CombatEncounterId] int IDENTITY(1,1) NOT NULL,
    [CampaignCampaignId] int  NOT NULL,
    [DateStartPlayed] datetime  NOT NULL,
    [DateEndPlayed] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [DateOfEncounter] datetime  NULL,
    [CurrentAP] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CombatEncounterItems'
CREATE TABLE [dbo].[CombatEncounterItems] (
    [CombatEncounterItemId] int IDENTITY(1,1) NOT NULL,
    [CombatEncounterCombatEncounterId] int  NOT NULL,
    [StartAp] nvarchar(max)  NOT NULL,
    [DurationAP] nvarchar(max)  NOT NULL,
    [SystemActionSystemActionId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CharacterCharacterId] int  NULL,
    [CombatantName] nvarchar(max)  NOT NULL,
    [Target] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CharacterId] in table 'Characters'
ALTER TABLE [dbo].[Characters]
ADD CONSTRAINT [PK_Characters]
    PRIMARY KEY CLUSTERED ([CharacterId] ASC);
GO

-- Creating primary key on [CampaignId] in table 'Campaigns'
ALTER TABLE [dbo].[Campaigns]
ADD CONSTRAINT [PK_Campaigns]
    PRIMARY KEY CLUSTERED ([CampaignId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [SystemRuleId] in table 'SystemRules'
ALTER TABLE [dbo].[SystemRules]
ADD CONSTRAINT [PK_SystemRules]
    PRIMARY KEY CLUSTERED ([SystemRuleId] ASC);
GO

-- Creating primary key on [SystemSkillId] in table 'SystemSkills'
ALTER TABLE [dbo].[SystemSkills]
ADD CONSTRAINT [PK_SystemSkills]
    PRIMARY KEY CLUSTERED ([SystemSkillId] ASC);
GO

-- Creating primary key on [ErrorId] in table 'ELMAH_Error'
ALTER TABLE [dbo].[ELMAH_Error]
ADD CONSTRAINT [PK_ELMAH_Error]
    PRIMARY KEY CLUSTERED ([ErrorId] ASC);
GO

-- Creating primary key on [SystemStatLevelId] in table 'SystemStatLevels'
ALTER TABLE [dbo].[SystemStatLevels]
ADD CONSTRAINT [PK_SystemStatLevels]
    PRIMARY KEY CLUSTERED ([SystemStatLevelId] ASC);
GO

-- Creating primary key on [SystemStatId] in table 'SystemStats'
ALTER TABLE [dbo].[SystemStats]
ADD CONSTRAINT [PK_SystemStats]
    PRIMARY KEY CLUSTERED ([SystemStatId] ASC);
GO

-- Creating primary key on [SystemSkillCategoryId] in table 'SystemSkillCategories'
ALTER TABLE [dbo].[SystemSkillCategories]
ADD CONSTRAINT [PK_SystemSkillCategories]
    PRIMARY KEY CLUSTERED ([SystemSkillCategoryId] ASC);
GO

-- Creating primary key on [SystemActionId] in table 'SystemActions'
ALTER TABLE [dbo].[SystemActions]
ADD CONSTRAINT [PK_SystemActions]
    PRIMARY KEY CLUSTERED ([SystemActionId] ASC);
GO

-- Creating primary key on [SystemActionCategoryId] in table 'SystemActionCategories'
ALTER TABLE [dbo].[SystemActionCategories]
ADD CONSTRAINT [PK_SystemActionCategories]
    PRIMARY KEY CLUSTERED ([SystemActionCategoryId] ASC);
GO

-- Creating primary key on [CombatEncounterId] in table 'CombatEncounters'
ALTER TABLE [dbo].[CombatEncounters]
ADD CONSTRAINT [PK_CombatEncounters]
    PRIMARY KEY CLUSTERED ([CombatEncounterId] ASC);
GO

-- Creating primary key on [CombatEncounterItemId] in table 'CombatEncounterItems'
ALTER TABLE [dbo].[CombatEncounterItems]
ADD CONSTRAINT [PK_CombatEncounterItems]
    PRIMARY KEY CLUSTERED ([CombatEncounterItemId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CampaignCampaignId] in table 'Characters'
ALTER TABLE [dbo].[Characters]
ADD CONSTRAINT [FK_CampaignCharacter]
    FOREIGN KEY ([CampaignCampaignId])
    REFERENCES [dbo].[Campaigns]
        ([CampaignId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CampaignCharacter'
CREATE INDEX [IX_FK_CampaignCharacter]
ON [dbo].[Characters]
    ([CampaignCampaignId]);
GO

-- Creating foreign key on [SystemRuleSystemRuleId] in table 'Campaigns'
ALTER TABLE [dbo].[Campaigns]
ADD CONSTRAINT [FK_SystemRulesCampaign]
    FOREIGN KEY ([SystemRuleSystemRuleId])
    REFERENCES [dbo].[SystemRules]
        ([SystemRuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemRulesCampaign'
CREATE INDEX [IX_FK_SystemRulesCampaign]
ON [dbo].[Campaigns]
    ([SystemRuleSystemRuleId]);
GO

-- Creating foreign key on [UserUserId] in table 'Characters'
ALTER TABLE [dbo].[Characters]
ADD CONSTRAINT [FK_UserCharacter]
    FOREIGN KEY ([UserUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCharacter'
CREATE INDEX [IX_FK_UserCharacter]
ON [dbo].[Characters]
    ([UserUserId]);
GO

-- Creating foreign key on [UserUserId] in table 'Campaigns'
ALTER TABLE [dbo].[Campaigns]
ADD CONSTRAINT [FK_UserCampaign]
    FOREIGN KEY ([UserUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCampaign'
CREATE INDEX [IX_FK_UserCampaign]
ON [dbo].[Campaigns]
    ([UserUserId]);
GO

-- Creating foreign key on [CampaignCampaignId] in table 'CombatEncounters'
ALTER TABLE [dbo].[CombatEncounters]
ADD CONSTRAINT [FK_CampaignCombatEncounter]
    FOREIGN KEY ([CampaignCampaignId])
    REFERENCES [dbo].[Campaigns]
        ([CampaignId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CampaignCombatEncounter'
CREATE INDEX [IX_FK_CampaignCombatEncounter]
ON [dbo].[CombatEncounters]
    ([CampaignCampaignId]);
GO

-- Creating foreign key on [SystemActionSystemActionId] in table 'CombatEncounterItems'
ALTER TABLE [dbo].[CombatEncounterItems]
ADD CONSTRAINT [FK_SystemActionCombatEncounterItem]
    FOREIGN KEY ([SystemActionSystemActionId])
    REFERENCES [dbo].[SystemActions]
        ([SystemActionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemActionCombatEncounterItem'
CREATE INDEX [IX_FK_SystemActionCombatEncounterItem]
ON [dbo].[CombatEncounterItems]
    ([SystemActionSystemActionId]);
GO

-- Creating foreign key on [CombatEncounterCombatEncounterId] in table 'CombatEncounterItems'
ALTER TABLE [dbo].[CombatEncounterItems]
ADD CONSTRAINT [FK_CombatEncounterCombatEncounterItem]
    FOREIGN KEY ([CombatEncounterCombatEncounterId])
    REFERENCES [dbo].[CombatEncounters]
        ([CombatEncounterId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CombatEncounterCombatEncounterItem'
CREATE INDEX [IX_FK_CombatEncounterCombatEncounterItem]
ON [dbo].[CombatEncounterItems]
    ([CombatEncounterCombatEncounterId]);
GO

-- Creating foreign key on [CharacterCharacterId] in table 'CombatEncounterItems'
ALTER TABLE [dbo].[CombatEncounterItems]
ADD CONSTRAINT [FK_CharacterCombatEncounterItem]
    FOREIGN KEY ([CharacterCharacterId])
    REFERENCES [dbo].[Characters]
        ([CharacterId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CharacterCombatEncounterItem'
CREATE INDEX [IX_FK_CharacterCombatEncounterItem]
ON [dbo].[CombatEncounterItems]
    ([CharacterCharacterId]);
GO

-- Creating foreign key on [SystemRuleSystemRuleId] in table 'SystemSkillCategories'
ALTER TABLE [dbo].[SystemSkillCategories]
ADD CONSTRAINT [FK_SystemRuleSystemSkillCategory]
    FOREIGN KEY ([SystemRuleSystemRuleId])
    REFERENCES [dbo].[SystemRules]
        ([SystemRuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemRuleSystemSkillCategory'
CREATE INDEX [IX_FK_SystemRuleSystemSkillCategory]
ON [dbo].[SystemSkillCategories]
    ([SystemRuleSystemRuleId]);
GO

-- Creating foreign key on [SystemRuleSystemRuleId] in table 'SystemStats'
ALTER TABLE [dbo].[SystemStats]
ADD CONSTRAINT [FK_SystemRuleSystemStat]
    FOREIGN KEY ([SystemRuleSystemRuleId])
    REFERENCES [dbo].[SystemRules]
        ([SystemRuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemRuleSystemStat'
CREATE INDEX [IX_FK_SystemRuleSystemStat]
ON [dbo].[SystemStats]
    ([SystemRuleSystemRuleId]);
GO

-- Creating foreign key on [SystemRuleSystemRuleId] in table 'SystemActionCategories'
ALTER TABLE [dbo].[SystemActionCategories]
ADD CONSTRAINT [FK_SystemRuleSystemActionCategory]
    FOREIGN KEY ([SystemRuleSystemRuleId])
    REFERENCES [dbo].[SystemRules]
        ([SystemRuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemRuleSystemActionCategory'
CREATE INDEX [IX_FK_SystemRuleSystemActionCategory]
ON [dbo].[SystemActionCategories]
    ([SystemRuleSystemRuleId]);
GO

-- Creating foreign key on [SystemActionCategorySystemActionCategoryId] in table 'SystemActions'
ALTER TABLE [dbo].[SystemActions]
ADD CONSTRAINT [FK_SystemActionCategorySystemAction]
    FOREIGN KEY ([SystemActionCategorySystemActionCategoryId])
    REFERENCES [dbo].[SystemActionCategories]
        ([SystemActionCategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemActionCategorySystemAction'
CREATE INDEX [IX_FK_SystemActionCategorySystemAction]
ON [dbo].[SystemActions]
    ([SystemActionCategorySystemActionCategoryId]);
GO

-- Creating foreign key on [SystemStatSystemStatId] in table 'SystemSkills'
ALTER TABLE [dbo].[SystemSkills]
ADD CONSTRAINT [FK_SystemStatSystemSkill]
    FOREIGN KEY ([SystemStatSystemStatId])
    REFERENCES [dbo].[SystemStats]
        ([SystemStatId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemStatSystemSkill'
CREATE INDEX [IX_FK_SystemStatSystemSkill]
ON [dbo].[SystemSkills]
    ([SystemStatSystemStatId]);
GO

-- Creating foreign key on [SystemSkillCategorySystemSkillCategoryId] in table 'SystemSkills'
ALTER TABLE [dbo].[SystemSkills]
ADD CONSTRAINT [FK_SystemSkillCategorySystemSkill]
    FOREIGN KEY ([SystemSkillCategorySystemSkillCategoryId])
    REFERENCES [dbo].[SystemSkillCategories]
        ([SystemSkillCategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemSkillCategorySystemSkill'
CREATE INDEX [IX_FK_SystemSkillCategorySystemSkill]
ON [dbo].[SystemSkills]
    ([SystemSkillCategorySystemSkillCategoryId]);
GO

-- Creating foreign key on [SystemStatSystemStatId] in table 'SystemStatLevels'
ALTER TABLE [dbo].[SystemStatLevels]
ADD CONSTRAINT [FK_SystemStatSystemStatLevel]
    FOREIGN KEY ([SystemStatSystemStatId])
    REFERENCES [dbo].[SystemStats]
        ([SystemStatId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemStatSystemStatLevel'
CREATE INDEX [IX_FK_SystemStatSystemStatLevel]
ON [dbo].[SystemStatLevels]
    ([SystemStatSystemStatId]);
GO

-- Creating foreign key on [SystemActionSystemActionId] in table 'SystemSkills'
ALTER TABLE [dbo].[SystemSkills]
ADD CONSTRAINT [FK_SystemActionSystemSkill]
    FOREIGN KEY ([SystemActionSystemActionId])
    REFERENCES [dbo].[SystemActions]
        ([SystemActionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemActionSystemSkill'
CREATE INDEX [IX_FK_SystemActionSystemSkill]
ON [dbo].[SystemSkills]
    ([SystemActionSystemActionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------