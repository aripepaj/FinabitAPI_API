SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spAccountList_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spAccountList_API] AS RETURN;');
GO

ALTER PROCEDURE dbo.spAccountList_API
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.Account,
        ISNULL(a.AccountDescription, N'') AS AccountDescription,
       ISNULL(a.Account, N'')
+ REPLICATE(N' ', CASE WHEN LEN(ISNULL(a.Account,N'')) < 6 THEN 6 - LEN(ISNULL(a.Account,N'')) ELSE 1 END)
+ N'- ' + ISNULL(a.AccountDescription, N'')
        + N' [ ' + ISNULL(s.AccountSubGroupName, N'') + N' ]' AS AccountDisplay,
        a.AccountSubGroupID,
        ISNULL(s.AccountSubGroupName, N'') AS AccountSubGroupName
    FROM dbo.tblAccount a
    JOIN dbo.tblAccountSubGroup s ON s.AccountSubGroupID = a.AccountSubGroupID
    WHERE a.AccountStatus = 'A'
      AND ISNULL(a.Active, 1) = 1
      AND ISNULL(a.Secret, 0) = 0
    ORDER BY a.Account;
END
