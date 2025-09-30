IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spItemsAdvancedExists_API]') AND type = 'P')
    EXEC('CREATE PROCEDURE dbo.spItemsAdvancedExists_API AS BEGIN SET NOCOUNT ON; END');
GO

-- Recreate with safe logic
ALTER PROCEDURE dbo.spItemsAdvancedExists_API1
      @DepartmentID  INT
    , @ItemID        NVARCHAR(200) = NULL
    , @ItemName      NVARCHAR(200) = NULL
    , @Barcode       NVARCHAR(200) = NULL
    , @ReturnDetails BIT = 0    -- 0: return only ID; 1: return full row
AS
BEGIN
    SET NOCOUNT ON;

    -- Treat 0 as NULL (no filter)
    DECLARE @DeptFilter INT = NULLIF(@DepartmentID, 0);

    DECLARE @HasDept BIT =
        CASE WHEN EXISTS (
            SELECT 1 FROM sys.columns
            WHERE object_id = OBJECT_ID('dbo.tblItems') AND name = 'DepartmentID'
        ) THEN 1 ELSE 0 END;

    DECLARE @HasBarcodes BIT =
        CASE WHEN OBJECT_ID('dbo.tblItemsBarcodes','U') IS NOT NULL THEN 1 ELSE 0 END;

    DECLARE @res TABLE
    (
        ID             INT,
        ItemID         NVARCHAR(200),
        ItemName       NVARCHAR(200),
        DepartmentID   INT            NULL,
        MatchedBarcode NVARCHAR(200)  NULL
    );

    DECLARE @sql NVARCHAR(MAX) = N'
        SELECT TOP (1)
              i.ID
            , i.ItemID
            , i.ItemName ' +
            CASE WHEN @HasDept = 1
                 THEN N', i.DepartmentID '
                 ELSE N', CAST(NULL AS INT) AS DepartmentID '
            END +
            CASE WHEN @HasBarcodes = 1 AND @Barcode IS NOT NULL
                 THEN N', b.Barcode AS MatchedBarcode '
                 ELSE N', CAST(NULL AS NVARCHAR(200)) AS MatchedBarcode '
            END + N'
        FROM dbo.tblItems AS i WITH (NOLOCK) ' +
            CASE WHEN @HasBarcodes = 1 AND @Barcode IS NOT NULL
                 THEN N'JOIN dbo.tblItemsBarcodes AS b WITH (NOLOCK) ON b.ItemID = i.ItemID
'
                 ELSE N''
            END + N'
        WHERE 1=1 ' +
            CASE WHEN @ItemID   IS NOT NULL THEN N' AND i.ItemID   = @pItemID '   ELSE N'' END +
            CASE WHEN @ItemName IS NOT NULL THEN N' AND i.ItemName LIKE N''%'' + @pItemName + N''%'' ' ELSE N'' END +
            CASE WHEN @Barcode  IS NOT NULL AND @HasBarcodes = 1 THEN N' AND b.Barcode = @pBarcode ' ELSE N'' END +
            CASE WHEN @Barcode  IS NOT NULL AND @HasBarcodes = 0 THEN N' AND 1 = 0 ' ELSE N'' END +
            CASE WHEN @HasDept  = 1 AND @DeptFilter IS NOT NULL THEN N' AND i.DepartmentID = @pDept ' ELSE N'' END + N'
        ORDER BY i.ID;';

    INSERT INTO @res (ID, ItemID, ItemName, DepartmentID, MatchedBarcode)
    EXEC sp_executesql
         @sql,
         N'@pDept INT, @pItemID NVARCHAR(200), @pItemName NVARCHAR(200), @pBarcode NVARCHAR(200), @DeptFilter INT',
         @pDept = @DeptFilter, @pItemID = @ItemID, @pItemName = @ItemName, @pBarcode = @Barcode, @DeptFilter = @DeptFilter;

    IF @ReturnDetails = 1
        SELECT TOP (1) ID, ItemID, ItemName, DepartmentID, MatchedBarcode
        FROM @res
        ORDER BY ID;
    ELSE
        SELECT ID = ISNULL((SELECT TOP 1 ID FROM @res ORDER BY ID), 0);
END
GO