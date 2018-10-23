CREATE PROCEDURE [dbo].[GetAllClients]
AS
	SELECT
		Id,
		ClientName
	FROM dbo.ClientAccount
