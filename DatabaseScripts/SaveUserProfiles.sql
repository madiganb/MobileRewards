CREATE PROCEDURE [dbo].[SaveUserProfiles]
	@Items AS dbo.UserProfileParam READONLY
AS
	BEGIN TRANSACTION;
	
	BEGIN TRY
		MERGE INTO dbo.UserProfile AS T
		USING @Items AS S
		ON T.Id = S.Id
		WHEN MATCHED THEN 
		UPDATE SET
			T.ClientAccountId = S.ClientAccountId,
			T.UserAccountId = S.UserAccountId,
			T.EmailAddress = S.EmailAddress
		WHEN NOT MATCHED THEN
		INSERT
		(
			Id,
			ClientAccountId,
			UserAccountId,
			EmailAddress
		)
		VALUES
		(
			S.Id,
			S.ClientAccountId,
			S.UserAccountId,
			S.EmailAddress
		);

		DELETE FROM dbo.ClientAccount WHERE Id NOT IN (SELECT Id FROM @Items);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		ELSE
			COMMIT TRANSACTION;
	END CATCH