/****** Object:  StoredProcedure [dbo].[spGetPartners_API]    Script Date: 9/16/2025 2:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spGetPartners_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spGetPartners_API] AS RETURN;');
GO


ALTER PROCEDURE [dbo].[spGetPartners_API] 
    @PartnerTypeID     int = 2,
    @PartnerName    nvarchar(200) = N'%',
    @PartnerGroup   nvarchar(200) = N'%',
	@PartnerCategory   nvarchar(200) = N'%',
	@PlaceName nvarchar(200) = N'%',
	@StateName nvarchar(200) = N'%'

AS

	SET NOCOUNT ON
	

declare @table table(AllValue money,Partnerid int)



		
	insert into @table 
	select 
			SUM(debit-credit),
			PartnerID 
	from tblJournalsDetails jd
	INNER JOIN dbo.tblAccount a ON a.Account = jd.Account
	where ISNULL(PartnerID,0) > 0
	AND AccountSubGroupID like case when @PartnerTypeID = 1 then 17 else 16 end
	group by partnerid

	select 	
		p.PartnerID,
		p.PartnerName as PartnerName,		
		CASE WHEN ISNULL(p.BusinessNo,'')='' THEN ' ' else p.BusinessNo END  as FiscalNo,
		ISNULL(p.RealBusinessNo,'') as BusinessNo,
		ISNULL(p.Address,'') as Address,
		ISNULL(p.Email,'') Email,
		ISNULL(pl.PlaceName ,' ') as PlaceName,
		ISNULL(ss.StateName,' ') StateName,
		ISNULL(pg.PartnerGroupName,' ') as [Group],
		ISNULL(pc.PartnerCategory,' ') as Category,
		ISNULL(p.DueDays , 0) as DueDays,
		ISNULL(p.DueValueMaximum  , 0) as DueValueMaximum,
		isnull(t.AllValue,0) as DueValue

	from dbo.tblPartners p		
	LEFT JOIN @table  as t on t.PartnerID = p.PartnerID
	LEFT JOIN tblPlaces pl ON pl.PlaceID = p.PlaceID
	LEFT JOIN tblpartnergroup pg on pg.[ID] = p.PartnerGroup
	left join tblPartnerCategories pc on pc.ID = p.PartnerCategoryID
	left join tblStates ss on ss.StateID = p.StateID
	where PartnerTypeID in (@PartnerTypeID) 	
	and PartnerName like '%' + @PartnerName + '%'
	and ISNULL(pg.PartnerGroupName,' ') like '%' + @PartnerGroup + '%'
	and ISNULL(pc.PartnerCategory,' ') like '%' + @PartnerCategory + '%'
	and ISNULL(ss.StateName,'') like '%' + @StateName + '%'
	and ISNULL(pl.PlaceName ,' ') like '%' + @PlaceName + '%'
	order by PartnerName asc
