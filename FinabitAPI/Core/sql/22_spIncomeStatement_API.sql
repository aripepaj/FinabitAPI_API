/****** Object:  StoredProcedure [dbo].[spIncomeStatement_API] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.spIncomeStatement_API', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.spIncomeStatement_API AS RETURN;');
GO

/*
EXEC dbo.spIncomeStatement_API 
    @prmFromDate='2025-01-01', 
    @prmEndDate='2025-12-31', 
    @filter = N'6000-Kostoja e mallit të shitur (KMSH)', 
    @IsMonthly=1;
*/

ALTER PROCEDURE [dbo].[spIncomeStatement_API]
    @prmFromDate  smalldatetime = NULL,
    @prmEndDate   smalldatetime = NULL,
    @filter       nvarchar(200) = N'',     -- NEW: text to search inside "Account - AccountDescription"
    @IsMonthly    bit           = 0        -- 0 = daily (default), 1 = monthly
AS
BEGIN
    SET NOCOUNT ON;

    CREATE TABLE #JournalDetails
    (
        Account varchar(100),
        [Date]  datetime,
        Total   money
    );

    /* 1) Collect totals per account per day (unchanged logic) */
    INSERT INTO #JournalDetails (Account, [Date], Total)
    SELECT 
        a.Account,
        jd.[Date],
        SUM(ISNULL(jd.Debit, 0) - ISNULL(jd.Credit, 0)) AS Total
    FROM dbo.tblJournalsDetails jd
    INNER JOIN dbo.tblAccount a ON a.Account = jd.Account
    WHERE (jd.[Date] >= @prmFromDate AND jd.[Date] <= @prmEndDate)
      AND a.AccountGroupID IN (4, 5, 7)
    GROUP BY a.Account, jd.[Date];

    SELECT
        a.Account + '-' + ISNULL(a.AccountDescription,'') AS Account,
        CASE 
            WHEN a.UseAfterNetoInProfitLoss = 1 
                THEN N'SHPENZIME PAS FITIMIT'
            ELSE UPPER(ag.AccountGroupName)
        END AS AccountGroup,
        CASE 
            WHEN @IsMonthly = 1 
                THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, jd.[Date]), 0)  
            ELSE CAST(jd.[Date] AS date)                              
        END AS [Date],
        -SUM(jd.Total) AS Total
    FROM #JournalDetails jd
    INNER JOIN dbo.tblAccount a       ON a.Account         = jd.Account
    INNER JOIN dbo.tblAccountGroup ag ON ag.AccountGroupID = a.AccountGroupID
    WHERE
        -- NEW: filter by concatenated Account - Description (null-safe)
        (ISNULL(@filter, N'') = N'' 
         OR (a.Account + N'-' + ISNULL(a.AccountDescription, N'')) LIKE N'%' + @filter + N'%')
    GROUP BY
        CASE 
            WHEN a.UseAfterNetoInProfitLoss = 1 
                THEN N'SHPENZIME PAS FITIMIT'
            ELSE UPPER(ag.AccountGroupName)
        END,
        a.Account + '-' + ISNULL(a.AccountDescription,''),
        CASE 
            WHEN @IsMonthly = 1 
                THEN DATEADD(MONTH, DATEDIFF(MONTH, 0, jd.[Date]), 0)
            ELSE CAST(jd.[Date] AS date)
        END
    ORDER BY
        [Date];

    DROP TABLE #JournalDetails;
END
GO
