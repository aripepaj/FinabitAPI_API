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

    ;WITH Invoiced AS
    (
        SELECT
            t.referenceid,
            td.ItemID,
            InvoicedQuantity = SUM(td.Quantity),
            cnt              = COUNT_BIG(*)
        FROM tblTransactions t
        INNER JOIN tblTransactionsDetails td ON td.TransactionID = t.ID
        WHERE ISNULL(t.ReferenceID, 0) <> 0
          AND t.TransactionTypeID IN (2)
          AND (@fd IS NULL OR t.[TransactionDate] >= DATEADD(day, -7,  @fd))
          AND (@td IS NULL OR t.[TransactionDate] <= DATEADD(day,  30, @td))
        GROUP BY t.referenceid, td.ItemID
    )
    SELECT
        t.[ID],
        t.[TransactionDate]  AS Data,
        t.[InvoiceNo]        AS Numri,
        p.PartnerID          AS ID_Konsumatorit,
        p.PartnerName        AS Konsumatori,
        d.DepartmentName     AS Komercialisti,
        CAST(CASE
                WHEN ISNULL(r.InvoicedQuantity, 0) = 0 AND ISNULL(r.cnt, 0) = 0 THEN N'Pa faturuar'
                WHEN ISNULL(r.InvoicedQuantity, 0) >= td.Quantity                   THEN N'Faturuar'
                ELSE N'Faturuar Pjesërisht'
             END AS varchar(30)) AS Statusi_Faturimit,
        td.ItemID            AS Shifra,
        i.ItemName           AS Emertimi,
        u.UnitName           AS Njesia_Artik,
        td.Quantity          AS Sasia,
        td.VATPrice          AS Cmimi
    FROM  dbo.tblTransactions t
    INNER JOIN tblTransactionsDetails td ON td.TransactionID = t.ID
    INNER JOIN tblItems i ON i.ItemID = td.ItemID
    INNER JOIN tblUnits u ON u.UnitID = i.UnitID
    LEFT  JOIN dbo.tblPartners   p ON p.PartnerID    = t.PartnerID
    LEFT  JOIN dbo.tblDepartment d ON d.DepartmentID = t.DepartmentID
    LEFT  JOIN Invoiced r ON r.referenceid = t.ID AND r.ItemID = td.ItemID
    WHERE (@fd IS NULL OR t.[TransactionDate] >= @fd)
      AND (@td IS NULL OR t.[TransactionDate] <= @td)
      AND t.TransactionTypeID = @TranTypeID
      AND td.ItemID      LIKE @ItemID
      AND i.ItemName     LIKE '%' + @ItemName + '%'
      AND p.PartnerName  LIKE '%' +@PartnerName + '%'
    ORDER BY t.ID DESC
    OPTION (RECOMPILE);
END
GO
