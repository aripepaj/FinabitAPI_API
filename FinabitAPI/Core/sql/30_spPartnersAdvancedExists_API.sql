-- Create stub if missing
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spPartnersAdvancedExists_API') AND type = 'P')
    EXEC('CREATE PROCEDURE dbo.spPartnersAdvancedExists_API AS BEGIN SET NOCOUNT ON; END');
GO

-- Recreate with minimal, safe logic
ALTER PROCEDURE dbo.spPartnersAdvancedExists_API
      @PartnerID   INT            = NULL
    , @PartnerName NVARCHAR(200)  = NULL
    , @Email       NVARCHAR(200)  = NULL
    , @BusinessNo  NVARCHAR(200)  = NULL  -- maps to p.RealBusinessNo (as in your SELECT)
    , @FiscalNo    NVARCHAR(200)  = NULL  -- maps to p.BusinessNo     (as in your SELECT)
AS
BEGIN
    SET NOCOUNT ON;

    -- Detect optional columns just in case schema differs
    DECLARE @HasRealBusinessNo BIT = CASE WHEN EXISTS (
        SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblPartners') AND name = 'RealBusinessNo'
    ) THEN 1 ELSE 0 END;

    DECLARE @HasBusinessNo BIT = CASE WHEN EXISTS (
        SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblPartners') AND name = 'BusinessNo'
    ) THEN 1 ELSE 0 END;

    DECLARE @sql NVARCHAR(MAX) = N'
        SELECT TOP (1) p.PartnerID
        FROM dbo.tblPartners AS p WITH (NOLOCK)
        WHERE 1 = 1 ' +
        CASE WHEN @PartnerID   IS NOT NULL THEN N' AND p.PartnerID   = @pPartnerID   ' ELSE N'' END +
        CASE WHEN @PartnerName IS NOT NULL THEN N' AND p.PartnerName = @pPartnerName ' ELSE N'' END +
        CASE WHEN @Email       IS NOT NULL THEN N' AND p.Email       = @pEmail       ' ELSE N'' END +
        CASE WHEN @BusinessNo  IS NOT NULL AND @HasRealBusinessNo = 1 THEN N' AND p.RealBusinessNo = @pBusinessNo ' ELSE N'' END +
        CASE WHEN @BusinessNo  IS NOT NULL AND @HasRealBusinessNo = 0 THEN N' AND 1 = 0 ' ELSE N'' END +
        CASE WHEN @FiscalNo    IS NOT NULL AND @HasBusinessNo    = 1 THEN N' AND p.BusinessNo     = @pFiscalNo   ' ELSE N'' END +
        CASE WHEN @FiscalNo    IS NOT NULL AND @HasBusinessNo    = 0 THEN N' AND 1 = 0 ' ELSE N'' END + N'
        ORDER BY p.PartnerID;';

    DECLARE @t TABLE (PartnerID INT);
    INSERT INTO @t (PartnerID)
    EXEC sp_executesql
         @sql,
         N'@pPartnerID INT, @pPartnerName NVARCHAR(200), @pEmail NVARCHAR(200), @pBusinessNo NVARCHAR(200), @pFiscalNo NVARCHAR(200)',
         @pPartnerID = @PartnerID, @pPartnerName = @PartnerName, @pEmail = @Email, @pBusinessNo = @BusinessNo, @pFiscalNo = @FiscalNo;

    SELECT ISNULL((SELECT TOP 1 PartnerID FROM @t), 0) AS PartnerID;
END
GO