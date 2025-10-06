/****** Ensure proc exists, then alter ******/
IF OBJECT_ID(N'[dbo].[spGeAccountMatches_API]', N'P') IS NULL
BEGIN
    EXEC(N'CREATE PROCEDURE [dbo].[spGeAccountMatches_API]
    AS
    BEGIN
        SET NOCOUNT ON;
        -- stub
    END');
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spGeAccountMatches_API]
    @AccountId   NVARCHAR(200) = N'',
    @AccountName NVARCHAR(200) = N''
AS
BEGIN
    SET NOCOUNT ON;

    SET @AccountId   = NULLIF(@AccountId,   N'');
    SET @AccountName = NULLIF(@AccountName, N'');

    SELECT
        2                                   AS DetailsType,
        a.Account                           AS ItemID,
        ISNULL(a.AccountDescription_2, N'') AS Description,
        ISNULL(a.AccountDescription,  N'')  AS ItemName
    FROM dbo.tblAccount a
    WHERE ISNULL(a.Secret, 0) = 0
      AND ISNULL(a.Active, 1) = 1
      AND (
            (@AccountId   IS NOT NULL AND a.Account            LIKE N'%' + @AccountId   + N'%')
         OR (@AccountName IS NOT NULL AND a.AccountDescription LIKE N'%' + @AccountName + N'%')
      )
    ORDER BY a.AccountDescription, a.Account;
END
GO
