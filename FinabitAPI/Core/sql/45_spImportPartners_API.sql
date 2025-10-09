
/****** Object:  StoredProcedure [dbo].[spImportPartners_API]    Script Date: 10/9/2025 8:48:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spImportPartners_API') AND type = 'P')
    EXEC('CREATE PROCEDURE dbo.spImportPartners_API AS BEGIN SET NOCOUNT ON; END');
GO

Alter PROCEDURE [dbo].[spImportPartners_API]
    @ImportPartners ImportPartners READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @userid INT,
            @FurnitorAccount VARCHAR(100),
            @KonsumatorAccount VARCHAR(100),
            @StateID INT;

    SELECT  @FurnitorAccount = '4001',
            @KonsumatorAccount = '2200'
    FROM tblOptions;

    SELECT TOP (1) @userid = UserID FROM tblUsers ORDER BY UserID;

    SET @StateID = 1;

    /* ===== Seed lookup tables (same approach as yours) ===== */
    INSERT INTO tblPartnerGroup (PartnerGroupName)
    SELECT DISTINCT i.PartnerGroup
    FROM @ImportPartners i
    WHERE LEN(i.PartnerGroup) > 0
      AND NOT EXISTS (SELECT 1 FROM tblPartnerGroup p WHERE p.PartnerGroupName = i.PartnerGroup);

    INSERT INTO [tblStates] ([StateName],[LUB],[LUD],[LUN],[InsDate]
    )
    SELECT DISTINCT i.[StateID], @userid, GETDATE(), @userid, GETDATE()
    FROM @ImportPartners i
    WHERE LEN(i.[StateID]) > 0
      AND NOT EXISTS (SELECT 1 FROM tblStates s WHERE s.StateName = i.[StateID]);

    INSERT INTO dbo.tblRegions (Region)
    SELECT DISTINCT i.Region
    FROM @ImportPartners i
    WHERE LEN(i.Region) > 0
      AND NOT EXISTS (SELECT 1 FROM tblRegions r WHERE r.Region = i.Region);

    INSERT INTO [tblPlaces] ([StateID],[PlaceName],[LUB],[LUD],[LUN],[InsDate])
    SELECT DISTINCT @StateID, LEFT(i.City,150), @userid, GETDATE(), @userid, GETDATE()
    FROM @ImportPartners i
    WHERE LEN(i.City) > 0
      AND NOT EXISTS (SELECT 1 FROM tblPlaces p WHERE p.PlaceName = LEFT(i.City,150))
    ORDER BY LEFT(i.City,150);

    INSERT INTO tblPartnerCategories ([PartnerCategory])
    SELECT DISTINCT i.[PartnerCategory]
    FROM @ImportPartners i
    WHERE LEN(i.[PartnerCategory]) > 0
      AND NOT EXISTS (SELECT 1 FROM tblPartnerCategories p WHERE p.[PartnerCategory] = i.[PartnerCategory]);

;WITH src AS
(
  SELECT
      i.PartnerName,
      PartnerTypeID = CASE
                        WHEN i.[Type] LIKE 'Fu%'   THEN 1
                        WHEN i.[Type] LIKE 'Ko%'   THEN 2
                        WHEN i.[Type] LIKE 'Kont%' THEN 3
                      END,
      AccountResolved =
          CASE
            WHEN i.[Type] LIKE 'Fu%' THEN CASE WHEN LEN(i.Account) > 1 THEN i.Account ELSE @FurnitorAccount END
            WHEN i.[Type] LIKE 'Ko%' THEN CASE WHEN LEN(i.Account) > 1 THEN i.Account ELSE @KonsumatorAccount END
          END,
      StateIDResolved  = (SELECT TOP 1 s.StateID  FROM tblStates  s WHERE s.StateName = i.[StateID]),
      PlaceIDResolved  = (SELECT TOP 1 p.PlaceID  FROM tblPlaces  p WHERE p.PlaceName = LEFT(i.City,150)),
      RegionIDResolved = (SELECT TOP 1 r.RegionID FROM tblRegions r WHERE r.Region    = i.Region),
      PartnerCategoryIDResolved = (SELECT TOP 1 pc.ID FROM tblPartnerCategories pc WHERE pc.PartnerCategory = i.PartnerCategory),
      PartnerGroupResolved = ISNULL((SELECT TOP 1 pg.ID FROM tblPartnerGroup pg WHERE pg.PartnerGroupName = i.PartnerGroup), 1),
      FiscalNoResolved = LEFT(i.FiscalNo, 20),
      AddressResolved  = i.[Adress],
      i.Tel1, i.Tel2, i.Email, i.[DiscountPercent], i.ItemID, i.ContactPerson, i.BussinesNo, i.VatNo
  FROM @ImportPartners i
)
UPDATE tgt
   SET tgt.[Account]           = COALESCE(NULLIF(LTRIM(RTRIM(src.AccountResolved)), ''), tgt.[Account]),
       tgt.[StateID]           = COALESCE(src.StateIDResolved,  tgt.[StateID]),
       tgt.[PlaceID]           = COALESCE(src.PlaceIDResolved,  tgt.[PlaceID]),
       tgt.[Tel1]              = COALESCE(NULLIF(LTRIM(RTRIM(src.Tel1)),  ''), tgt.[Tel1]),
       tgt.[Tel2]              = COALESCE(NULLIF(LTRIM(RTRIM(src.Tel2)),  ''), tgt.[Tel2]),
       tgt.[Email]             = COALESCE(NULLIF(LTRIM(RTRIM(src.Email)), ''), tgt.[Email]),
       tgt.[LUN]               = COALESCE(tgt.[LUN], 0), -- leave as-is; ensures not NULL
       tgt.[LUB]               = @userid,
       tgt.[LUD]               = GETDATE(),
       tgt.[BusinessNo]        = COALESCE(NULLIF(LTRIM(RTRIM(src.FiscalNoResolved)), ''), tgt.[BusinessNo]),
       tgt.[DiscountPercent]   = COALESCE(src.[DiscountPercent], tgt.[DiscountPercent]),
       tgt.[Address]           = COALESCE(NULLIF(LTRIM(RTRIM(src.AddressResolved)), ''), tgt.[Address]),
       tgt.[ItemID]            = COALESCE(src.[ItemID], tgt.[ItemID]),
       tgt.[ContactPerson]     = COALESCE(NULLIF(LTRIM(RTRIM(src.ContactPerson)), ''), tgt.[ContactPerson]),
       tgt.[RegionID]          = COALESCE(src.RegionIDResolved, tgt.[RegionID]),
       tgt.[PartnerCategoryID] = COALESCE(src.PartnerCategoryIDResolved, tgt.[PartnerCategoryID]),
       tgt.[PartnerGroup]      = COALESCE(src.PartnerGroupResolved, tgt.[PartnerGroup]),
       tgt.[RealBusinessNo]    = COALESCE(NULLIF(LTRIM(RTRIM(src.BussinesNo)), ''), tgt.[RealBusinessNo]),
       tgt.[VATNO]             = COALESCE(NULLIF(LTRIM(RTRIM(src.VatNo)), ''), tgt.[VATNO]),
       tgt.[AllowSaleWithMaxDue] = tgt.[AllowSaleWithMaxDue] -- keep current setting
FROM tblPartners tgt
JOIN src
  ON tgt.PartnerName   = src.PartnerName
 AND tgt.PartnerTypeID = src.PartnerTypeID
WHERE src.PartnerTypeID IS NOT NULL;

    /* ===== INSERT new partners (fixed column count + City length) ===== */
    INSERT INTO [dbo].[tblPartners]
           ([PartnerName]
           ,[PartnerTypeID]
           ,[Account]
           ,[StateID]
           ,[PlaceID]
           ,[Tel1]
           ,[Tel2]
           ,[Fax]
           ,[Email]
           ,[WebSite]
           ,[NIPT]
           ,[Active]
           ,[InsBy]
           ,[InsDate]
           ,[LUN]
           ,[LUB]
           ,[LUD]
           ,[BusinessNo]
           ,[PersonalNo]
           ,[OwnerID]
           ,[BankAccount]
           ,[DiscountPercent]
           ,[PIN]
           ,[Address]
           ,[ItemID]
           ,[Corporate]
           ,[FirstName]
           ,[LastName]
           ,[Gender]
           ,[MartialStatus]
           ,[MaritalStatus]
           ,[ContactPerson]
           ,[PriceMenuID]
           ,[RegionID]
           ,[DueDays]
           ,[DueValueMaximum]
           ,[ContractNo]
           ,[PartnerCategoryID]
           ,[Evaluation]
           ,[PartnerGroup]
           ,[RealBusinessNo]
           ,[ICCOM]
           ,[DiscountLevel]
           ,[VATNO]
           ,[AllowSaleWithMaxDue])
    SELECT DISTINCT
           i.PartnerName
         , CASE
               WHEN i.[Type] LIKE 'Fu%'   THEN 1
               WHEN i.[Type] LIKE 'Ko%'   THEN 2
               WHEN i.[Type] LIKE 'Kont%' THEN 3
           END
         , CASE
               WHEN i.[Type] LIKE 'Fu%' THEN CASE WHEN LEN(i.Account) > 1 THEN i.Account ELSE @FurnitorAccount END
               WHEN i.[Type] LIKE 'Ko%' THEN CASE WHEN LEN(i.Account) > 1 THEN i.Account ELSE @KonsumatorAccount END
           END
         , (SELECT TOP 1 s.StateID FROM tblStates s WHERE s.StateName = i.[StateID])
         , (SELECT TOP 1 p.PlaceID FROM tblPlaces p WHERE p.PlaceName = LEFT(i.City,150))
         , i.Tel1
         , i.Tel2
         , ''
         , i.Email
         , ''
         , ''
         , 1
         , @userid
         , GETDATE()
         , 0
         , @userid
         , GETDATE()
         , LEFT(i.FiscalNo,20)
         , ''
         , ''
         , ''
         , i.[DiscountPercent]
         , ''
         , [Adress]
         , i.[ItemID]
         , ''
         , ''
         , ''
         , ''
         , ''    -- [MartialStatus]
         , ''    -- [MaritalStatus]  (extra '' to match insert column list)
         , i.ContactPerson
         , ''
         , (SELECT TOP 1 r.RegionID FROM tblRegions r WHERE r.Region = i.Region)
         , ''
         , ''
         , ''
         , (SELECT TOP 1 pc.ID FROM tblPartnerCategories pc WHERE pc.PartnerCategory = i.PartnerCategory)
         , ''
         , ISNULL((SELECT TOP 1 pg.ID FROM tblPartnerGroup pg WHERE pg.PartnerGroupName = i.PartnerGroup),1)
         , i.BussinesNo
         , ''
         , ''
         , i.VatNo
         , 1
    FROM @ImportPartners i
    LEFT JOIN tblPartners pp
      ON  pp.PartnerName   = i.PartnerName
      AND pp.PartnerTypeID = CASE
                               WHEN i.[Type] LIKE 'Fu%'   THEN 1
                               WHEN i.[Type] LIKE 'Ko%'   THEN 2
                               WHEN i.[Type] LIKE 'Kont%' THEN 3
                             END
    WHERE pp.PartnerID IS NULL
      AND (CASE
             WHEN i.[Type] LIKE 'Fu%'   THEN 1
             WHEN i.[Type] LIKE 'Ko%'   THEN 2
             WHEN i.[Type] LIKE 'Kont%' THEN 3
           END) IS NOT NULL
    ORDER BY i.PartnerName;
END
GO