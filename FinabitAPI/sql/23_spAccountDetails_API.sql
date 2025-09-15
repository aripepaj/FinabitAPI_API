/****** Object:  StoredProcedure [dbo].[spAccountDetails_API]    Script Date: 9/15/2025 9:47:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

exec [spAccountDetails_API] @FromDate='2025-01-01',@ToDate='2025-09-01',@Account='10028%'

*/

IF OBJECT_ID(N'dbo.[spAccountDetails_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spAccountDetails_API] AS RETURN;');
GO

ALTER PROCEDURE [dbo].[spAccountDetails_API]
		@FromDate smalldatetime,
		@ToDate smalldatetime,
		@Account varchar(100) 

AS

set @FromDate = @FromDate
set @ToDate = @ToDate


	SELECT 
		t.TransactionType,
		jd.Date as Date,
		jd.Description,
		j.Number,		
		CASE WHEN ISNULL(p.PartnerNAme,'') <> '' THEN ISNULL(p.PartnerNAme,'') ELSE ISNULL(e.FirstName,'') + ' ' + ISNULL(e.LastName,'') END as NAme,
		a.Account + ' - ' + AccountDescription as Account,
		ISNULL(jd.Debit,0)/(CASE WHEN jd.CurrencyRate = 0 THEN 1 ELSE ISNULL(jd.CurrencyRate,1) END)  as DebitValue,
		ISNULL(jd.Credit,0)/(CASE WHEN jd.CurrencyRate = 0 THEN 1 ELSE ISNULL(jd.CurrencyRate,1) END)  as CreditValue
	FROM dbo.tblJournals j 
	INNER JOIN dbo.tblJournalsDetails jd ON jd.JournalID = j.JournalID
	INNER JOIN dbo.tblTransactionsType t ON t.ID = j.TransactionType
	INNER JOIN dbo.tblAccount a ON a.Account = jd.Account
	LEFT JOIN dbo.tblPartners p ON p.PartnerID = jd.PartnerID
	LEFT JOIN dbo.tblEmployees e ON e.EmpID = jd.EmpID
	WHERE (jd.Date >= @FromDate AND jd.Date <=  @ToDate)
	AND a.Account+a.AccountDescription Like '%' + @Account + '%'
	AND isnull(Secret,0) =  0
	ORDER BY  Date