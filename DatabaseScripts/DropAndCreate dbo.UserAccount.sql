/****** Object: Table [dbo].[UserAccount] Script Date: 10/16/2018 11:37:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[UserAccount];


GO
CREATE TABLE [dbo].[UserAccount] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Username]  NVARCHAR (255)   NOT NULL,
    [Password]  NVARCHAR (255)   NOT NULL
);

ALTER TABLE [dbo].[UserAccount]
	ADD CONSTRAINT PK_UserAccountId PRIMARY KEY CLUSTERED (Id);