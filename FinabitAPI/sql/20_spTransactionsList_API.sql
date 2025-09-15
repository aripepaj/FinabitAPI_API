-- 020_spTransactionsList_API.sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create stub if missing (for old servers without CREATE OR ALTER)
IF OBJECT_ID(N'dbo.spTransactionsList_API', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.spTransactionsList_API AS RETURN;');
GO

ALTER PROCEDURE [dbo].[spTransactionsList_API]
    @FromDate       varchar(100),
    @ToDate         varchar(100),
    @TranTypeID     int,
    @ItemID         nvarchar(200) = N'%',
    @ItemName       nvarchar(200) = N'%',
    @PartnerName    nvarchar(200) = N'%'
AS
BEGIN
    SET NOCOUNT ON;

    IF @ItemID = N''      SET @ItemID = N'%';
    IF @ItemName = N''    SET @ItemName = N'%';
    IF @PartnerName = N'' SET @PartnerName = N'%';

    -- Use datetime + CONVERT/ISDATE for older servers
    DECLARE @fd datetime, @td datetime;

    -- Try ISO 8601 (126), then ODBC canonical (120), then implicit
    IF ISDATE(@FromDate) = 1 SET @fd = CONVERT(datetime, @FromDate, 126);
    IF @fd IS NULL AND ISDATE(@FromDate) = 1 SET @fd = CONVERT(datetime, @FromDate, 120);
    IF @fd IS NULL AND ISDATE(@FromDate) = 1 SET @fd = CONVERT(datetime, @FromDate);

    IF ISDATE(@ToDate) = 1 SET @td = CONVERT(datetime, @ToDate, 126);
    IF @td IS NULL AND ISDATE(@ToDate) = 1 SET @td = CONVERT(datetime, @ToDate, 120);
    IF @td IS NULL AND ISDATE(@ToDate) = 1 SET @td = CONVERT(datetime, @ToDate);

    DECLARE @snapshot_isolation_state tinyint;
    SELECT @snapshot_isolation_state = snapshot_isolation_state
    FROM sys.databases
    WHERE name = DB_NAME();

    IF @fd IS NOT NULL AND @td IS NOT NULL
    BEGIN
        IF ABS(DATEDIFF(day, @fd, @td)) > 31 AND @snapshot_isolation_state = 1
            SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
    END


    SELECT
        t.[ID],
        t.[TransactionDate]  AS Data,
        t.[InvoiceNo]        AS Numri,
        p.PartnerID          AS ID_Konsumatorit,
        p.PartnerName        AS Konsumatori,
        d.DepartmentName     AS Komercialisti,
        ''AS Statusi_Faturimit,
        td.ItemID            AS Shifra,
        ISNULL(i.ItemName, a.AccountDescription)           AS Emertimi,
        u.UnitName           AS Njesia_Artik,
        td.Quantity          AS Sasia,
        td.VATPrice          AS Cmimi,
		td.PriceWithDiscount AS SalesPrice,
		td.Price AS CostPrice
    FROM  dbo.tblTransactions t
    INNER JOIN tblTransactionsDetails td ON td.TransactionID = t.ID
    LEFT JOIN tblItems i ON i.ItemID = td.ItemID
	LEFT JOIN dbo.tblAccount a          ON a.Account        = td.ItemID
    LEFT JOIN tblUnits u ON u.UnitID = i.UnitID
    LEFT  JOIN dbo.tblPartners   p ON p.PartnerID    = t.PartnerID
    LEFT  JOIN dbo.tblDepartment d ON d.DepartmentID = t.DepartmentID
    WHERE (@fd IS NULL OR t.[TransactionDate] >= @fd)
      AND (@td IS NULL OR t.[TransactionDate] <= @td)
      AND t.TransactionTypeID = @TranTypeID
      AND td.ItemID      LIKE @ItemID
      AND ISNULL(i.ItemName, a.AccountDescription)     LIKE '%' + @ItemName + '%'
      AND p.PartnerName  LIKE '%' +@PartnerName + '%'
    ORDER BY t.ID DESC
    OPTION (RECOMPILE);
END
