CREATE PROCEDURE [dbo].[SaveAwardItems]
	@Items AS dbo.AwardItemParam READONLY
AS
	BEGIN TRANSACTION;
	
	BEGIN TRY
		MERGE INTO dbo.AwardItem AS T
		USING @Items AS S
		ON T.Id = S.Id
		WHEN MATCHED THEN 
		UPDATE SET
			T.UserProfileId = S.UserProfileId,
			T.AwardStatusId = S.AwardStatusId,
			T.AwardName = S.AwardName,
			T.EarnedValue = S.EarnedValue,
			T.CurrentValue = S.CurrentValue
		WHEN NOT MATCHED THEN
		INSERT
		(
			Id,
			UserProfileId,
			AwardStatusId,
			AwardName,
			EarnedValue,
			CurrentValue
		)
		VALUES
		(
			S.Id,
			S.UserProfileId,
			S.AwardStatusId,
			S.AwardName,
			S.EarnedValue,
			S.CurrentValue
		);

		DELETE FROM dbo.AwardItem WHERE Id NOT IN (SELECT Id FROM @Items);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		ELSE
			COMMIT TRANSACTION;
	END CATCH