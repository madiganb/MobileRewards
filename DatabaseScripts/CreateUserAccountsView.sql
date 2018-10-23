CREATE VIEW [dbo].[vw_UserAccounts]
AS
	SELECT
		UA.Id AS UserAccountId,
		UA.FirstName,
		UA.LastName,
		UA.Username,
		UA.[Password],
		UP.Id AS UserProfileId,
		UP.EmailAddress,
		CA.Id AS ClientAccountId,
		CA.ClientName,
		AI.Id AS AwardItemId,
		AI.AwardStatusId,
		AI.AwardName,
		AI.EarnedValue,
		AI.CurrentValue
	FROM dbo.UserAccount AS UA
	LEFT OUTER JOIN dbo.UserProfile AS UP ON UA.Id = UP.UserAccountId
	LEFT OUTER JOIN dbo.ClientAccount AS CA ON UP.ClientAccountId = CA.Id
	LEFT OUTER JOIN dbo.AwardItem AS AI ON UP.Id = AI.UserProfileId