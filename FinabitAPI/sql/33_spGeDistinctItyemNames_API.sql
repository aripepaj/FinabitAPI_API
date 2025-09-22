/****** Object:  StoredProcedure [dbo].[spGeDistinctItyemNames_API]    Script Date: 9/17/2025 3:23:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'[dbo].[spGeDistinctItyemNames_API]', N'P') IS NULL
BEGIN
    EXEC(N'CREATE PROCEDURE [dbo].[spGeDistinctItyemNames_API]
    AS
    BEGIN
        SET NOCOUNT ON;
        -- stub
    END');
END
GO

ALTER procedure [dbo].[spGeDistinctItyemNames_API]
    @ItemID     nvarchar(200) = '',
	@ItemName     nvarchar(200) = ''
as


select distinct
	DetailsType,
	td.ItemID ItemID,
	ISNULL(td.ItemName,'') as Description,
	isnull(i.ItemName,a.AccountDescription) ItemName
from tblTransactionsDetails td
left join tblItems i on i.ItemID = td.ItemID
left join tblAccount a on a.Account = td.ItemID
where DetailsType in (1,2)
and (td.ItemID like '%' + @ItemID + '%' 
  OR ISNULL(i.barcode1,'') like '%' + @ItemID + '%'
  OR  ISNULL(td.ItemName,'') like '%' + @ItemName + '%'
  OR  isnull(i.ItemName,a.AccountDescription) like '%' + @ItemName + '%'
 )


