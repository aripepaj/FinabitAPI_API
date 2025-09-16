SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spGetItemsAll_Filtered_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spGetItemsAll_Filtered_API] AS RETURN;');
GO

ALTER PROCEDURE [dbo].[spGetItemsAll_Filtered_API]
    @ItemName     nvarchar(200) = NULL,
    @ItemID       nvarchar(100) = NULL,
    @ItemGroupID  int           = NULL,
    @ItemGroup    nvarchar(200) = NULL,
    @Barcode      nvarchar(100) = NULL,
    @Category     nvarchar(200) = NULL,
    @Prodhuesi    nvarchar(200) = NULL,
    @Active       bit           = NULL,
    @PageNumber   int           = 1,
    @PageSize     int           = 50
AS
BEGIN
    SET NOCOUNT ON;

    -- normalize empties to NULL
    IF (@ItemName   = N'') SET @ItemName = NULL;
    IF (@ItemID     = N'') SET @ItemID = NULL;
    IF (@ItemGroup  = N'') SET @ItemGroup = NULL;
    IF (@Barcode    = N'') SET @Barcode = NULL;
    IF (@Category   = N'') SET @Category = NULL;
    IF (@Prodhuesi  = N'') SET @Prodhuesi = NULL;

    -- paging bounds (defensive)
    IF (@PageNumber IS NULL OR @PageNumber < 1) SET @PageNumber = 1;
    IF (@PageSize   IS NULL OR @PageSize   < 1) SET @PageSize   = 50;

    DECLARE @StartRow int = ((@PageNumber - 1) * @PageSize) + 1;
    DECLARE @EndRow   int =  (@PageNumber * @PageSize);

    ;WITH base AS
    (
        SELECT
            v.Id,
            ISNULL(v.ItemID, '')           AS ItemID,
            ISNULL(v.ItemName, '')         AS ItemName,
            ISNULL(u.UnitName, '')         AS UnitName,
            ISNULL(u.UnitID, 0)            AS UnitID,
            ISNULL(v.ItemGroupID, 0)       AS ItemGroupID,
            ISNULL(itg.ItemGroup, '')      AS ItemGroup,
            ISNULL(v.Taxable, 0)           AS Taxable,
            ISNULL(v.Active, 0)            AS Active,
            ISNULL(v.Dogana, 0)            AS Dogana,
            ISNULL(v.Akciza, 0)            AS Akciza,
            ISNULL(v.Color, '')            AS Color,
            ISNULL(v.PDAItemName, '')      AS PDAItemName,
            ISNULL(v.VATValue, 0)          AS VATValue,
            ISNULL(v.AkcizaValue, 0)       AS AkcizaValue,
            ISNULL(v.MaximumQuantity, 0)   AS MaximumQuantity,
            ISNULL(v.Coefficient, 0)       AS Coefficient,
            ISNULL(v.barcode1, '')         AS barcode1,
            ISNULL(pr.Shifra, '')          AS barcode2,   -- producer code as barcode2
            ISNULL(v.SalesPrice2, 0)       AS SalesPrice2,
            ISNULL(v.SalesPrice3, 0)       AS SalesPrice3,
            ISNULL(v.Origin, '')           AS Origin,
            ISNULL(v.Category, '')         AS Category,
            ISNULL(v.PLU, '')              AS PLU,
            ISNULL(v.ItemTemplate, '')     AS ItemTemplate,
            ISNULL(v.Weight, 0)            AS Weight,
            ISNULL(v.Author, '')           AS Author,
            ISNULL(v.Publisher, '')        AS Publisher,
            ISNULL(v.CustomField1, '')     AS CustomField1,
            ISNULL(v.CustomField2, '')     AS CustomField2,
            ISNULL(v.CustomField3, '')     AS CustomField3,
            ISNULL(v.CustomField4, '')     AS CustomField4,
            ISNULL(v.CustomField5, '')     AS CustomField5,
            ISNULL(v.CustomField6, '')     AS CustomField6,
            ISNULL(v.Barcode3, '')         AS Barcode3,
            ISNULL(v.NettoBruttoWeight,0)  AS NettoBruttoWeight,
            ISNULL(v.BrutoWeight, 0)       AS BrutoWeight,
            ISNULL(v.MaxDiscount, 0)       AS MaxDiscount,
            ISNULL(pr.Shifra, 0)           AS ShifraProdhuesit,
            ISNULL(pr.Emri, '')            AS Prodhuesi
        FROM tblItems v
        LEFT JOIN tblProdhuesi pr ON pr.ID = v.ProdhuesiID
        LEFT JOIN tblUnits u      ON u.UnitID = v.UnitID
        LEFT JOIN tblItemGroup itg ON itg.ID = v.ItemGroupID
    ),
    filtered AS
    (
        SELECT
            b.*,
            ROW_NUMBER() OVER (ORDER BY b.ItemName, b.ItemID) AS rn,
            COUNT(1) OVER () AS TotalCount
        FROM base b
        WHERE
            (@Active IS NULL OR b.Active = @Active)
            AND (@ItemGroupID IS NULL OR b.ItemGroupID = @ItemGroupID)
            AND (@ItemGroup  IS NULL OR b.ItemGroup LIKE '%' + @ItemGroup + '%')
            AND (@ItemID     IS NULL OR b.ItemID    LIKE '%' + @ItemID    + '%')
            AND (@ItemName   IS NULL OR b.ItemName  LIKE '%' + @ItemName  + '%')
            AND (@Category   IS NULL OR b.Category  LIKE '%' + @Category  + '%')
            AND (
                  @Prodhuesi IS NULL
                  OR b.Prodhuesi LIKE '%' + @Prodhuesi + '%'
                  OR CAST(b.ShifraProdhuesit AS nvarchar(200)) LIKE '%' + @Prodhuesi + '%'
                )
            AND (
                  @Barcode IS NULL
                  OR b.barcode1 LIKE '%' + @Barcode + '%'
                  OR b.Barcode3 LIKE '%' + @Barcode + '%'
                  OR b.PLU      LIKE '%' + @Barcode + '%'
                  OR CAST(b.ShifraProdhuesit AS nvarchar(100)) LIKE '%' + @Barcode + '%'
                )
    )
    SELECT
        Id, ItemID, ItemName, UnitName, UnitID, ItemGroupID, ItemGroup,
        Taxable, Active, Dogana, Akciza, Color, PDAItemName, VATValue, AkcizaValue,
        MaximumQuantity, Coefficient, barcode1, barcode2, SalesPrice2, SalesPrice3,
        Origin, Category, PLU, ItemTemplate, Weight, Author, Publisher,
        CustomField1, CustomField2, CustomField3, CustomField4, CustomField5, CustomField6,
        Barcode3, NettoBruttoWeight, BrutoWeight, MaxDiscount, ShifraProdhuesit, Prodhuesi,
        TotalCount
    FROM filtered
    WHERE rn BETWEEN @StartRow AND @EndRow
    ORDER BY rn;
END
GO
