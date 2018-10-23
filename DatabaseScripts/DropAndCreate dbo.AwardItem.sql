/****** Object: Table [dbo].[AwardItem] Script Date: 10/16/2018 11:36:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[AwardItem];


GO
CREATE TABLE [dbo].[AwardItem] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [UserProfileId] UNIQUEIDENTIFIER NOT NULL,
    [AwardStatusId] INT              NOT NULL,
    [AwardName]     NVARCHAR (255)   NOT NULL,
    [EarnedValue]   DECIMAL (10, 2)  NOT NULL,
    [CurrentValue]  DECIMAL (10, 2)  NOT NULL
);

ALTER TABLE [dbo].[AwardItem]
	ADD CONSTRAINT PK_AwardItemId PRIMARY KEY CLUSTERED (Id);

ALTER TABLE [dbo].[AwardItem]
    ADD CONSTRAINT [FK_AwardItem_AwardStatus] FOREIGN KEY ([AwardStatusId]) REFERENCES [dbo].[AwardStatus] ([Id]);

ALTER TABLE [dbo].[AwardItem]
    ADD CONSTRAINT [FK_AwardItem_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [dbo].[UserProfile] ([Id]);