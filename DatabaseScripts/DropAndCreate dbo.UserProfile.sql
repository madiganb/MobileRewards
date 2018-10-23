/****** Object: Table [dbo].[UserProfile] Script Date: 10/16/2018 11:38:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[UserProfile];


GO
CREATE TABLE [dbo].[UserProfile] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [ClientAccountId] UNIQUEIDENTIFIER NOT NULL,
    [UserAccountId]   UNIQUEIDENTIFIER NOT NULL,
    [EmailAddress]    NVARCHAR (255)   NOT NULL
);

ALTER TABLE [dbo].[UserProfile]
	ADD CONSTRAINT PK_UserProfileId PRIMARY KEY CLUSTERED (Id);

ALTER TABLE [dbo].[UserProfile]
    ADD CONSTRAINT [FK_UserProfile_ClientAccount] FOREIGN KEY ([ClientAccountId]) REFERENCES [dbo].[ClientAccount] ([Id]);

ALTER TABLE [dbo].[UserProfile]
    ADD CONSTRAINT [FK_UserProfile_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [dbo].[UserAccount] ([Id]);