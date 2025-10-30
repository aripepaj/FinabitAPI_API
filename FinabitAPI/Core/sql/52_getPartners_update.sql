/****** Object:  StoredProcedure [dbo].[spGetPartners_API] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spGetPartners_API] 
    @PartnerTypeID   int = 2,
    @PartnerName     nvarchar(200) = N'%',     -- e.g. 'IP KOS SH.P.K'
    @PartnerGroup    nvarchar(200) = N'%',
    @PartnerCategory nvarchar(200) = N'%',
    @PlaceName       nvarchar(200) = N'%',
    @StateName       nvarchar(200) = N'%'
AS
BEGIN
    SET NOCOUNT ON;

    -- normalize the incoming search once
    DECLARE @SearchNorm nvarchar(200) =
        UPPER(@PartnerName) COLLATE Latin1_General_CI_AI;  -- case/accent insensitive
    SET @SearchNorm = REPLACE(@SearchNorm, ' ', '');
    SET @SearchNorm = REPLACE(@SearchNorm, '-', '');
    SET @SearchNorm = REPLACE(@SearchNorm, '.', '');
    SET @SearchNorm = REPLACE(@SearchNorm, '(', '');
    SET @SearchNorm = REPLACE(@SearchNorm, ')', '');

    DECLARE @table table(AllValue money, PartnerID int);

    INSERT INTO @table
    SELECT SUM(debit - credit), PartnerID
    FROM tblJournalsDetails jd
    INNER JOIN dbo.tblAccount a ON a.Account = jd.Account
    WHERE ISNULL(PartnerID,0) > 0
      AND AccountSubGroupID LIKE CASE WHEN @PartnerTypeID = 1 THEN 17 ELSE 16 END
    GROUP BY PartnerID;

    SELECT
        p.PartnerID,
        p.PartnerName,
        CASE WHEN ISNULL(p.BusinessNo,'')='' THEN ' ' ELSE p.BusinessNo END  as FiscalNo,
        ISNULL(p.RealBusinessNo,'') as BusinessNo,
        ISNULL(p.Address,'') as Address,
        ISNULL(p.Email,'') as Email,
        ISNULL(pl.PlaceName,' ') as PlaceName,
        ISNULL(ss.StateName,' ') as StateName,
        ISNULL(pg.PartnerGroupName,' ') as [Group],
        ISNULL(pc.PartnerCategory,' ') as Category,
        ISNULL(p.DueDays,0) as DueDays,
        ISNULL(p.DueValueMaximum,0) as DueValueMaximum,
        ISNULL(t.AllValue,0) as DueValue
    FROM dbo.tblPartners p
    LEFT JOIN @table t               ON t.PartnerID = p.PartnerID
    LEFT JOIN tblPlaces pl           ON pl.PlaceID = p.PlaceID
    LEFT JOIN tblPartnerGroup pg     ON pg.ID = p.PartnerGroup
    LEFT JOIN tblPartnerCategories pc ON pc.ID = p.PartnerCategoryID
    LEFT JOIN tblStates ss           ON ss.StateID = p.StateID
    WHERE PartnerTypeID IN (@PartnerTypeID)
      AND ISNULL(pg.PartnerGroupName,' ') LIKE '%' + @PartnerGroup + '%'
      AND ISNULL(pc.PartnerCategory,' ') LIKE '%' + @PartnerCategory + '%'
      AND ISNULL(ss.StateName,'')         LIKE '%' + @StateName + '%'
      AND ISNULL(pl.PlaceName,' ')        LIKE '%' + @PlaceName + '%'
      AND REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
              UPPER(p.PartnerName) COLLATE Latin1_General_CI_AI,
              ' ', ''), '-', ''), '.', ''), '(', ''), ')', '')
          LIKE '%' + @SearchNorm + '%'
    ORDER BY PartnerName ASC;
END