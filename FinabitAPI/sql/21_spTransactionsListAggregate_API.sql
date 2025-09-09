SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create stub if missing (for old servers without CREATE OR ALTER)
IF OBJECT_ID(N'dbo.spTransactionsListAggregate_API', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.spTransactionsListAggregate_API AS RETURN;');
GO

USE [FINA_PRINCE]
GO
/****** Object:  StoredProcedure [dbo].[spTransactionsListAggregate_API]    Script Date: 09.09.25 4:08:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spTransactionsListAggregate_API]
    @FromDate        varchar(100),
    @ToDate          varchar(100),
    @TranTypeID      int,
    @ItemID          nvarchar(200) = N'%',
    @ItemName        nvarchar(200) = N'%',
    @PartnerName     nvarchar(200) = N'%',
    @DepartmentName  nvarchar(200) = N''   -- only department name
AS
BEGIN
    SET NOCOUNT ON;

    IF @ItemID = N''      SET @ItemID = N'%';
    IF @ItemName = N''    SET @ItemName = N'%';
    IF @PartnerName = N'' SET @PartnerName = N'%';

    DECLARE @fd datetime, @td datetime;

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
          AND (@fd IS NULL OR t.[TransactionDate] >= DATEADD(day, -7, @fd))
          AND (@td IS NULL OR t.[TransactionDate] <= DATEADD(day, 30, @td))
        GROUP BY t.referenceid, td.ItemID
    )
    SELECT
        t.[TransactionDate]                AS Data,
        SUM(td.Quantity * td.VATPrice)     AS Value,
        COUNT(*)                           AS rows
    FROM dbo.tblTransactions t
    INNER JOIN tblTransactionsDetails td ON td.TransactionID = t.ID
    LEFT JOIN tblItems i       ON i.ItemID       = td.ItemID
    LEFT JOIN tblAccount a     ON a.Account      = td.ItemID
    LEFT JOIN tblUnits u       ON u.UnitID       = i.UnitID
    LEFT JOIN dbo.tblPartners p ON p.PartnerID   = t.PartnerID
    LEFT JOIN dbo.tblDepartment d ON d.DepartmentID = t.DepartmentID
    LEFT JOIN Invoiced r ON r.referenceid = t.ID AND r.ItemID = td.ItemID
    WHERE (@fd IS NULL OR t.[TransactionDate] >= @fd)
      AND (@td IS NULL OR t.[TransactionDate] <= @td)
      AND t.TransactionTypeID = @TranTypeID
      AND td.ItemID LIKE @ItemID
      AND ISNULL(i.ItemName, a.AccountDescription) LIKE '%' + @ItemName + '%'
      AND p.PartnerName LIKE '%' + @PartnerName + '%'
      AND (ISNULL(@DepartmentName, N'') = N''
           OR d.DepartmentName LIKE '%' + @DepartmentName + '%')
    GROUP BY t.[TransactionDate]
    ORDER BY t.[TransactionDate] DESC
    OPTION (RECOMPILE);
END
GO
