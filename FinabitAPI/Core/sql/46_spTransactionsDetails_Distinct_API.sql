
/****** Object:  StoredProcedure [dbo].[spTransactionsDetails_Distinct_API]    Script Date: 10/9/2025 8:48:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spTransactionsDetails_Distinct_API') AND type = 'P')
    EXEC('CREATE PROCEDURE dbo.spTransactionsDetails_Distinct_API AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.spTransactionsDetails_Distinct_API
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Base AS
    (
        SELECT
            td.ID,
            td.TransactionID,
            td.DetailsType,
            td.ItemID,
            td.ItemName,
            td.Account,
            td.Value,
            td.TransactionNo,
            td.TransactionDate,
            td.Memo,
            t.PartnerID
        FROM dbo.tblTransactionsDetails td
        INNER JOIN dbo.tblTransactions t
            ON t.ID = td.TransactionID
        WHERE td.DetailsType IN (3,4,5,6)
    ),
    Ranked AS
    (
        SELECT
            b.*,
            -- Normalizations used in the dedupe key
            CAST(b.TransactionDate AS date)                AS TxDateOnly,
            ROUND(COALESCE(b.Value, 0.0), 2)               AS AmountRounded,
            UPPER(LTRIM(RTRIM(COALESCE(b.ItemName, ''))))  AS ItemNameNorm,
            UPPER(LTRIM(RTRIM(COALESCE(b.Memo, ''))))      AS MemoNorm,
            UPPER(LTRIM(RTRIM(COALESCE(b.Account, ''))))   AS AccountNorm,
            CASE
                WHEN UPPER(LTRIM(RTRIM(COALESCE(b.Account, '')))) <> '' 
                     AND UPPER(LTRIM(RTRIM(COALESCE(b.Account, '')))) <> '0'
                THEN 1 ELSE 0
            END AS AccountIsValid,
            ROW_NUMBER() OVER
            (
                PARTITION BY
                    -- base key
                    b.DetailsType,
                    CAST(b.TransactionDate AS date),
                    ROUND(COALESCE(b.Value, 0.0), 2),
                    UPPER(LTRIM(RTRIM(COALESCE(b.ItemName, '')))),

                    -- discriminator: PartnerID (if >0) else Account (if valid) else Memo
                    CASE WHEN b.PartnerID IS NOT NULL AND b.PartnerID > 0 THEN b.PartnerID END,
                    CASE WHEN (b.PartnerID IS NULL OR b.PartnerID <= 0) AND
                               (UPPER(LTRIM(RTRIM(COALESCE(b.Account, '')))) <> '' AND UPPER(LTRIM(RTRIM(COALESCE(b.Account, '')))) <> '0')
                         THEN UPPER(LTRIM(RTRIM(COALESCE(b.Account, '')))) END,
                    CASE WHEN (b.PartnerID IS NULL OR b.PartnerID <= 0) AND
                               NOT (UPPER(LTRIM(RTRIM(COALESCE(b.Account, '')))) <> '' AND UPPER(LTRIM(RTRIM(COALESCE(b.Account, '')))) <> '0')
                         THEN UPPER(LTRIM(RTRIM(COALESCE(b.ItemName, '')))) END

                ORDER BY b.TransactionDate DESC, b.ID DESC
            ) AS rn
        FROM Base b
    )
    SELECT TOP (300)
        ID,
        TransactionID,
        DetailsType,
        ItemID,
        ItemName,
        Account,
        Value       AS Amount,
        TransactionNo,
        TransactionDate,
        PartnerID,
        Memo
    FROM Ranked
    WHERE rn = 1
    ORDER BY TransactionDate DESC, ID DESC;
END
GO