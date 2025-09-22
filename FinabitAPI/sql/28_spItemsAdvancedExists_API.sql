IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spItemsAdvancedExists_API]') AND type = 'P')
    EXEC('CREATE PROCEDURE dbo.spItemsAdvancedExists_API AS BEGIN SET NOCOUNT ON; END');
GO

-- Recreate with safe logic
ALTER PROCEDURE dbo.spItemsAdvancedExists_API
      @DepartmentID INT                         -- kept for API, used only if column exists
    , @ItemID       NVARCHAR(200) = NULL
    , @ItemName     NVARCHAR(200) = NULL
    , @Barcode      NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HasDept BIT = CASE
        WHEN EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblItems') AND name = 'DepartmentID') THEN 1
        ELSE 0 END;

    DECLARE @HasBarcodes BIT = CASE
        WHEN OBJECT_ID('dbo.tblItemsBarcodes','U') IS NOT NULL THEN 1
        ELSE 0 END;

    DECLARE @sql NVARCHAR(MAX) = N'
        SELECT TOP (1) i.ID
        FROM dbo.tblItems AS i WITH (NOLOCK) ' +
        CASE WHEN @HasBarcodes = 1 AND @Barcode IS NOT NULL
             THEN N'JOIN dbo.tblItemsBarcodes AS b WITH (NOLOCK) ON b.ItemID = i.ItemID
             '
             ELSE N''
        END + N'
        WHERE 1=1 ' +
        CASE WHEN @ItemID   IS NOT NULL THEN N' AND i.ItemID   = @pItemID '   ELSE N'' END +
        CASE WHEN @ItemName IS NOT NULL THEN N' AND i.ItemName = @pItemName ' ELSE N'' END +
        CASE WHEN @Barcode  IS NOT NULL AND @HasBarcodes = 1 THEN N' AND b.Barcode = @pBarcode ' ELSE N'' END +
        CASE WHEN @Barcode  IS NOT NULL AND @HasBarcodes = 0 THEN N' AND 1 = 0 ' ELSE N'' END +
        CASE WHEN @HasDept  = 1 THEN N' AND i.DepartmentID = @pDept ' ELSE N'' END + N'
        ORDER BY i.ID;';

    DECLARE @t TABLE (ID INT);
    INSERT INTO @t (ID)
    EXEC sp_executesql
         @sql,
         N'@pDept INT, @pItemID NVARCHAR(200), @pItemName NVARCHAR(200), @pBarcode NVARCHAR(200)',
         @pDept = @DepartmentID, @pItemID = @ItemID, @pItemName = @ItemName, @pBarcode = @Barcode;

    SELECT ID = ISNULL((SELECT TOP 1 ID FROM @t), 0);
END
GO