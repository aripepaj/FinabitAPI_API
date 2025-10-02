/****** Object:  StoredProcedure [dbo].[spGeDistinctItyemNames_API]    Script Date: 9/17/2025 3:23:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'[dbo].[spGeDistinctItyemNames_API]', N'P') IS NULL
BEGIN
    EXEC(N'CREATE PROCEDURE [dbo].[spGeDistinctItyemNames_API]
    AS
    BEGIN
        SET NOCOUNT ON;
        -- stub
    END');
END
GO

ALTER PROCEDURE [dbo].[spGeDistinctItyemNames_API]
    @ItemID   nvarchar(200) = NULL,
    @ItemName nvarchar(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        td.DetailsType,
        ISNULL(td.ItemID, '') AS ItemID,
        ISNULL(td.ItemName, '') AS Description,
        ISNULL(i.ItemName, a.AccountDescription) AS ItemName,
        CAST(ISNULL(i.VATValue, 0) AS decimal(18,2)) AS VatValue
    FROM tblTransactionsDetails td
    LEFT JOIN tblItems  i ON i.ItemID  = td.ItemID
    LEFT JOIN tblAccount a ON a.Account = td.ItemID
    WHERE td.DetailsType IN (1,2)
      AND (
            @ItemID IS NULL
            OR ISNULL(td.ItemID, '')    LIKE '%' + @ItemID + '%'
            OR ISNULL(i.barcode1, '')   LIKE '%' + @ItemID + '%'
          )
      AND (
            @ItemName IS NULL
            OR ISNULL(td.ItemName, '')                          LIKE '%' + @ItemName + '%'
            OR ISNULL(i.ItemName, a.AccountDescription)         LIKE '%' + @ItemName + '%'
          );
END
