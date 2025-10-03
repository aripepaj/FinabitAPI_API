SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spAccountLookup_API') AND type = 'P')
    EXEC('CREATE PROCEDURE dbo.spAccountLookup_API AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.spAccountLookup_API
    @SubGroup INT   -- must be 6 or 7
AS
BEGIN
    SET NOCOUNT ON;

    IF @SubGroup NOT IN (6, 7)
    BEGIN
        RAISERROR('Invalid @SubGroup. Use 6 or 7.', 16, 1);
        RETURN;
    END

    SELECT
        a.Account,
        ISNULL(a.AccountDescription, N'') AS AccountDescription,
        -- Nice display text for dropdowns
        ISNULL(a.Account, N'')
        + REPLICATE(N' ', CASE WHEN LEN(ISNULL(a.Account, N'')) < 6 THEN 6 - LEN(ISNULL(a.Account, N'')) ELSE 1 END)
        + N'- ' + ISNULL(a.AccountDescription, N'')
        + N' [ ' + ISNULL(s.AccountSubGroupName, N'') + N' ]' AS AccountDisplay,
        a.AccountSubGroupID,
        ISNULL(s.AccountSubGroupName, N'') AS AccountSubGroupName
    FROM dbo.tblAccount a
    INNER JOIN dbo.tblAccountSubGroup s
        ON s.AccountSubGroupID = a.AccountSubGroupID
    WHERE a.AccountStatus = 'A'
      AND ISNULL(a.Active, 1) = 1
      AND ISNULL(a.Secret, 0) = 0
      AND a.AccountSubGroupID = @SubGroup
    ORDER BY a.Account;
END
GO