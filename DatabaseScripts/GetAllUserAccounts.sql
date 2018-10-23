CREATE PROCEDURE [dbo].[GetAllUserAccounts]
AS
	SELECT
		UserAccountId,
		FirstName,
		LastName,
		Username,
		[Password],
		UserProfileId,
		EmailAddress,
		ClientAccountId,
		ClientName,
		AwardItemId,
		AwardStatusId,
		AwardName,
		EarnedValue,
		CurrentValue
	FROM vw_UserAccounts
