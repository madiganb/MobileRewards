CREATE PROCEDURE [dbo].[SaveClientAccounts]
	@Items AS dbo.ClientAccountParam READONLY
AS
	BEGIN TRANSACTION;
	
	BEGIN TRY
		MERGE INTO dbo.ClientAccount AS T
		USING @Items AS S
		ON T.Id = S.Id
		WHEN MATCHED THEN 
		UPDATE SET
			T.ClientName = S.ClientName
		WHEN NOT MATCHED THEN
		INSERT
		(
			Id,
			ClientName
		)
		VALUES
		(
			S.Id,
			S.ClientName
		);

		DELETE FROM dbo.ClientAccount WHERE Id NOT IN (SELECT Id FROM @Items);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION;
				THROW;
			END
		ELSE
			COMMIT TRANSACTION;
	END CATCH