/****** Object:  StoredProcedure [dbo].[_ImportItems] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spGetLastDistinctItemNames_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spGetLastDistinctItemNames_API] AS RETURN;');
GO

ALTER PROCEDURE [dbo].[spGetLastDistinctItemNames_API]
    @Limit int = 300
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Ranked AS (
        SELECT
            td.DetailsType,
            ISNULL(td.ItemID, '') AS ItemID,
            ISNULL(td.ItemName, '') AS Description,
            ISNULL(i.ItemName, a.AccountDescription) AS ItemName,
            CAST(ISNULL(i.VATValue, 0) AS decimal(18,2)) AS VatValue,

            td.ID AS LastSeenId,

            ROW_NUMBER() OVER (
                PARTITION BY
                    td.DetailsType,
                    ISNULL(td.ItemID, ''),
                    ISNULL(td.ItemName, ''),
                    ISNULL(i.ItemName, a.AccountDescription)
                ORDER BY
                    td.ID DESC               -- recency (highest ID = latest)
            ) AS rn
        FROM tblTransactionsDetails td
        LEFT JOIN tblItems   i ON i.ItemID  = td.ItemID
        LEFT JOIN tblAccount a ON a.Account = td.ItemID
        WHERE td.DetailsType IN (1,2)
    )
    SELECT TOP (@Limit)
        DetailsType,
        ItemID,
        Description,
        ISNULL(ItemName, '') AS ItemName,
        VatValue
    FROM Ranked
    WHERE rn = 1
    ORDER BY
        LastSeenId DESC;                 -- newest first
END
GO