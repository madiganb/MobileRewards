CREATE PROCEDURE [dbo].[SaveUserAccounts]
	@Items AS dbo.UserAccountParam READONLY
AS
	BEGIN TRANSACTION;
	
	BEGIN TRY
		MERGE INTO dbo.UserAccount AS T
		USING @Items AS S
		ON T.Id = S.Id
		WHEN MATCHED THEN 
		UPDATE SET
			T.FirstName = S.LastName,
			T.LastName = S.LastName,
			T.Username = S.Username,
			T.[Password] = ISNULL(S.[Password], T.[Password])
		WHEN NOT MATCHED THEN
		INSERT
		(
			Id,
			FirstName,
			LastName,
			Username,
			[Password]
		)
		VALUES
		(
			S.Id,
			S.FirstName,
			S.LastName,
			S.Username,
			S.[Password]
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