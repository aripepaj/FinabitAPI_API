
/****** Object:  StoredProcedure [dbo].[spItemsAdvancedExists_API]    Script Date: 10/1/2025 4:34:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spItemsAdvancedExists_API') AND type = 'P')
    EXEC('CREATE PROCEDURE dbo.spItemsAdvancedExists_API AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE [dbo].[spItemsAdvancedExists_API]
      @DepartmentID  INT
    , @ItemID        NVARCHAR(200) = NULL
    , @ItemName      NVARCHAR(200) = NULL
    , @Barcode       NVARCHAR(200) = NULL
    , @ReturnDetails BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- normalize blanks -> NULL
    SET @ItemID   = NULLIF(LTRIM(RTRIM(@ItemID)),   N'');
    SET @ItemName = NULLIF(LTRIM(RTRIM(@ItemName)), N'');
    SET @Barcode  = NULLIF(LTRIM(RTRIM(@Barcode)),  N'');

    DECLARE @DeptFilter INT = NULLIF(@DepartmentID, 0);

    DECLARE @HasDept BIT =
        CASE WHEN EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblItems') AND name = 'DepartmentID') THEN 1 ELSE 0 END;

    DECLARE @HasBarcodes BIT =
        CASE WHEN OBJECT_ID('dbo.tblItemsBarcodes','U') IS NOT NULL THEN 1 ELSE 0 END;

    DECLARE @res TABLE
    (
        ID             INT,
        ItemID         NVARCHAR(200),
        ItemName       NVARCHAR(200),
        VatValue       DECIMAL(18,2) NULL,
        DepartmentID   INT           NULL,
        MatchedBarcode NVARCHAR(200) NULL
    );

    DECLARE @sql NVARCHAR(MAX) = N'
        SELECT TOP (1)
              i.ID
            , i.ItemID
            , i.ItemName
            , CAST(ISNULL(i.VATValue, 0) AS DECIMAL(18,2)) AS VatValue ' +             -- <<< add
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
                 ELSE N'' END + N'
        WHERE 1=1 ' +
            CASE WHEN @ItemID   IS NOT NULL THEN N' AND i.ItemID   = @pItemID '   ELSE N'' END +
            CASE WHEN @ItemName IS NOT NULL THEN N' AND i.ItemName LIKE N''%'' + @pItemName + N''%'' ' ELSE N'' END +
            CASE WHEN @Barcode  IS NOT NULL AND @HasBarcodes = 1 THEN N' AND b.Barcode = @pBarcode ' ELSE N'' END +
            CASE WHEN @Barcode  IS NOT NULL AND @HasBarcodes = 0 THEN N' AND 1 = 0 ' ELSE N'' END +
            CASE WHEN @HasDept  = 1 AND @DeptFilter IS NOT NULL THEN N' AND i.DepartmentID = @pDept ' ELSE N'' END + N'
        ORDER BY i.ID;';

    INSERT INTO @res (ID, ItemID, ItemName, VatValue, DepartmentID, MatchedBarcode)  -- <<< add VatValue
    EXEC sp_executesql
         @sql,
         N'@pDept INT, @pItemID NVARCHAR(200), @pItemName NVARCHAR(200), @pBarcode NVARCHAR(200)',
         @pDept = @DeptFilter, @pItemID = @ItemID, @pItemName = @ItemName, @pBarcode = @Barcode;

    IF @ReturnDetails = 1
        SELECT TOP (1) ID, ItemID, ItemName, VatValue, DepartmentID, MatchedBarcode   -- <<< include VatValue
        FROM @res
        ORDER BY ID;
    ELSE
        SELECT ID = ISNULL((SELECT TOP 1 ID FROM @res ORDER BY ID), 0);
END
