/****** Object: Table [dbo].[AwardStatus] Script Date: 10/16/2018 11:37:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[AwardStatus];


GO
CREATE TABLE [dbo].[AwardStatus] (
    [Id]              INT           NOT NULL,
    [AwardStatusName] NVARCHAR (50) NOT NULL
);

ALTER TABLE [dbo].[AwardStatus]
	ADD CONSTRAINT PK_AwardStatusId PRIMARY KEY CLUSTERED (Id);

INSERT INTO [dbo].[AwardStatus]
(
    [Id],
    [AwardStatusName]
)
VALUES
(1, 'Active'),
(2, 'PartiallyRedeemed'),
(3, 'FullyRedeemed'),
(4, 'RevokedDueToError'),
(5, 'RevokedDueToFraud')