
/****** Object:  StoredProcedure [dbo].[spIncomeStatement_API]    Script Date: 9/10/2025 4:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID(N'dbo.spIncomeStatement_API', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.spIncomeStatement_API AS RETURN;');
GO

/*

EXEC [spIncomeStatement_API]  '2025-01-01','2025-12-31'

*/


ALTER PROCEDURE [dbo].[spIncomeStatement_API]
					@prmFromDate smalldatetime = null,
					@prmEndDate smalldatetime = null
					

AS


create table #JournalDetails (Account varchar(100),Date datetime,Total money)




		-- Nxjerrja shumave totale sipas llogarise
		insert into #JournalDetails
		select 
			a.Account,
			jd.date,
			sum(isnull(Debit,0)-isnull(Credit,0)) as Total
		from tblJournalsDetails jd
		INNER JOIn tblAccount A on A.Account = JD.Account
		where (Date >= @prmFromDate and Date <= @prmEndDate)
		and A.AccountGroupID  IN (4,5,7) 
		group by a.account,jd.date


	




		-- REZULTATI
		SELECT  
			a.account + '-' + a.accountdescription as Account,
			case when a.UseAfterNetoInProfitLoss = 1 
				then 'SHPENZIME PAS FITIMIT'
			else UPPER(ag.AccountGroupName) end  AccountGroup,
			jd.Date,
			-sum(Total)  as Total
		from #JournalDetails jd
		inner join tblAccount a on a.Account = jd.Account
		inner join tblAccountGroup ag on ag.AccountGroupID = a.AccountGroupID
		group by 
				jd.Date,
				a.account + '-' + a.accountdescription,
				case when a.UseAfterNetoInProfitLoss = 1 
					then 'SHPENZIME PAS FITIMIT'
				else UPPER(ag.AccountGroupName) end

	