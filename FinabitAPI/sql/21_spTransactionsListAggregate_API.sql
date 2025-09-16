/****** Object:  StoredProcedure [dbo].[spTransactionsListAggregate_API] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spTransactionsListAggregate_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spTransactionsListAggregate_API] AS RETURN;');
GO

ALTER PROCEDURE [dbo].[spTransactionsListAggregate_API]
    @FromDate        varchar(100),
    @ToDate          varchar(100),
    @TranTypeID      int,
    @ItemID          nvarchar(200) = N'%',
    @ItemName        nvarchar(200) = N'%',
    @PartnerName     nvarchar(200) = N'%',
    @LocationName    nvarchar(200) = N'%',
    @IsMonthly       bit           = 0      -- 0 = daily (default), 1 = monthly
AS
BEGIN
    SET NOCOUNT ON;

    IF @ItemID = N''      SET @ItemID = N'%';
    IF @ItemName = N''    SET @ItemName = N'%';
    IF @PartnerName = N'' SET @PartnerName = N'%';
	IF @LocationName = N''   SET @LocationName = N'%';

    DECLARE @fd datetime, @td datetime;

    -- Try several ISO/ODBC parse styles to be tolerant of inputs
    IF ISDATE(@FromDate) = 1 SET @fd = CONVERT(datetime, @FromDate, 126);
    IF @fd IS NULL AND ISDATE(@FromDate) = 1 SET @fd = CONVERT(datetime, @FromDate, 120);
    IF @fd IS NULL AND ISDATE(@FromDate) = 1 SET @fd = CONVERT(datetime, @FromDate);

    IF ISDATE(@ToDate) = 1 SET @td = CONVERT(datetime, @ToDate, 126);
    IF @td IS NULL AND ISDATE(@ToDate) = 1 SET @td = CONVERT(datetime, @ToDate, 120);
    IF @td IS NULL AND ISDATE(@ToDate) = 1 SET @td = CONVERT(datetime, @ToDate);

    -- Elevate isolation if the range is larger than ~1 month and DB supports snapshot
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
        CASE 
            WHEN @IsMonthly = 1 
                -- First day of the month (works on SQL Server 2008+)
                THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, t.[TransactionDate]), 0)
            ELSE CAST(t.[TransactionDate] AS date)  -- group by day (strip time)
        END AS Data,
		d.DepartmentName AS LocationName,
        SUM(td.Quantity * td.VATPrice)          AS Value,
        SUM(td.Quantity * td.PriceWithDiscount) AS ValueWIthoutVat, -- keep existing alias
        SUM(td.Quantity * td.Price)             AS CostValue,
        COUNT(*)                                AS rows
    FROM dbo.tblTransactions t
    INNER JOIN dbo.tblTransactionsDetails td ON td.TransactionID = t.ID
    LEFT JOIN dbo.tblItems i            ON i.ItemID         = td.ItemID
    LEFT JOIN dbo.tblAccount a          ON a.Account        = td.ItemID
    LEFT JOIN dbo.tblUnits u            ON u.UnitID         = i.UnitID
    LEFT JOIN dbo.tblPartners p         ON p.PartnerID      = t.PartnerID
    LEFT JOIN dbo.tblDepartment d       ON d.DepartmentID   = t.DepartmentID
    WHERE (@fd IS NULL OR t.[TransactionDate] >= @fd)
      AND (@td IS NULL OR t.[TransactionDate] <= @td)
      AND t.TransactionTypeID = @TranTypeID
      AND td.ItemID LIKE @ItemID
      AND ISNULL(i.ItemName, a.AccountDescription) LIKE '%' + @ItemName + '%'
      AND p.PartnerName LIKE '%' + @PartnerName + '%'
      AND (ISNULL(@LocationName, N'') = N'' OR d.DepartmentName LIKE '%' + @LocationName + '%')
    GROUP BY
        CASE 
            WHEN @IsMonthly = 1 
                THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, t.[TransactionDate]), 0)
            ELSE CAST(t.[TransactionDate] AS date)
        END,
		 d.DepartmentName 
    ORDER BY
        CASE 
            WHEN @IsMonthly = 1 
                THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, t.[TransactionDate]), 0)
            ELSE CAST(t.[TransactionDate] AS date)
        END DESC
    OPTION (RECOMPILE);
END
