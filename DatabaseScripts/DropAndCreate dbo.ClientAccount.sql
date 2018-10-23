/****** Object: Table [dbo].[ClientAccount] Script Date: 10/16/2018 11:37:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[ClientAccount];


GO
CREATE TABLE [dbo].[ClientAccount] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ClientName] NVARCHAR (255)   NOT NULL
);

ALTER TABLE [dbo].[ClientAccount]
	ADD CONSTRAINT PK_ClientAccountId PRIMARY KEY CLUSTERED (Id);