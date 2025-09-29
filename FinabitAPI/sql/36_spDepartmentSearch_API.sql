SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[spDepartmentSearch_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[spDepartmentSearch_API] AS RETURN;');
GO

ALTER PROCEDURE [dbo].[spDepartmentSearch_API]
(
    @IsCombo BIT = 1,
    @Search  NVARCHAR(100) = NULL
)
AS
SET NOCOUNT ON;

DECLARE @q NVARCHAR(100) = NULLIF(LTRIM(RTRIM(@Search)), N'');

BEGIN
    IF (@IsCombo = 1)
    BEGIN
        ;WITH filtered AS
        (
            SELECT 
                d.DepartmentID,
                d.DepartmentName,
                ISNULL(d.PriceMenuID,'')                 AS PriceMenuID,
                ISNULL(p.Name,'')                        AS PriceMenuName,
                ISNULL(d.CompanyID,'')                   AS CompanyID,
                ISNULL(c.Company,'')                     AS Company,
                ISNULL(d.StartCount,'')                  AS StartCount,
                ISNULL(d.EndCount,'')                    AS EndCount,
                ISNULL(d.LUB,0)                          AS LUB,
                ISNULL(d.LUD,'2000-01-01')               AS LUD,
                d.AllowNegative,
                ISNULL(d.LUN,0)                          AS LUN,
                d.Account,
                d.Active,
                d.DepartmentTypeID,
                dt.DepartmentType,
                ISNULL(d.UsePaletBarcodes,0)             AS UsePaletBarcodes,
                d.Address,
                ISNULL(d.UseAkciza,0)                    AS UseAkciza,
                ISNULL(d.MulliriID,0)                    AS MulliriID,
                ISNULL(d.AllowPromotion,0)               AS AllowPromotion,
                ISNULL(d.DownloadPartnersByDepartment,0) AS DownloadPartnersByDepartment,
                d.InsBy,
                CASE WHEN d.DepartmentID = c.DefaultDepID THEN 1 ELSE 0 END AS IsDefaultDepartment
            FROM dbo.tblDepartment AS d
            LEFT JOIN dbo.tblPriceMenu      AS p  ON p.ID  = d.PriceMenuID
            LEFT JOIN dbo.tblDepartmentType AS dt ON dt.ID = d.DepartmentTypeID
            LEFT JOIN dbo.tblCompany        AS c  ON c.ID  = d.CompanyID
            WHERE d.DepartmentID > 0
              -- NOTE: only search when @q IS NOT NULL; if NULL, filtered is empty
              AND (@q IS NOT NULL AND d.DepartmentName LIKE '%' + @q + '%')
        ),
        fallback AS
        (
            SELECT TOP (1)
                d.DepartmentID,
                d.DepartmentName,
                ISNULL(d.PriceMenuID,'')                 AS PriceMenuID,
                ISNULL(p.Name,'')                        AS PriceMenuName,
                ISNULL(d.CompanyID,'')                   AS CompanyID,
                ISNULL(c.Company,'')                     AS Company,
                ISNULL(d.StartCount,'')                  AS StartCount,
                ISNULL(d.EndCount,'')                    AS EndCount,
                ISNULL(d.LUB,0)                          AS LUB,
                ISNULL(d.LUD,'2000-01-01')               AS LUD,
                d.AllowNegative,
                ISNULL(d.LUN,0)                          AS LUN,
                d.Account,
                d.Active,
                d.DepartmentTypeID,
                dt.DepartmentType,
                ISNULL(d.UsePaletBarcodes,0)             AS UsePaletBarcodes,
                d.Address,
                ISNULL(d.UseAkciza,0)                    AS UseAkciza,
                ISNULL(d.MulliriID,0)                    AS MulliriID,
                ISNULL(d.AllowPromotion,0)               AS AllowPromotion,
                ISNULL(d.DownloadPartnersByDepartment,0) AS DownloadPartnersByDepartment,
                d.InsBy,
                1 AS IsDefaultDepartment
            FROM dbo.tblDepartment AS d
            LEFT JOIN dbo.tblPriceMenu      AS p  ON p.ID  = d.PriceMenuID
            LEFT JOIN dbo.tblDepartmentType AS dt ON dt.ID = d.DepartmentTypeID
            LEFT JOIN dbo.tblCompany        AS c  ON c.ID  = d.CompanyID
            WHERE d.DepartmentID > 0
              AND d.DepartmentID = c.DefaultDepID
            ORDER BY ISNULL(c.[OrderBy], 0), d.DepartmentName
        ),
        chosen AS
        (
            SELECT * FROM filtered
            UNION ALL
            SELECT * FROM fallback
            WHERE NOT EXISTS (SELECT 1 FROM filtered)
        )
        SELECT 
            0 AS DepartmentID, 
            '-- Te gjitha --' AS DepartmentName,
            0 AS PriceMenuID,
            '' AS PriceMenuName,
            0 AS CompanyID,
            '' AS Company,
            '' AS StartCount,
            '' AS EndCount,
            0 AS LUB,
            CAST('2079-05-08 12:35:29.998' AS smalldatetime) AS LUD,
            0 AS AllowNegative,
            0 AS LUN,
            '' AS Account,
            0 AS Active,
            0 AS DepartmentTypeID,
            '' AS DepartmentType,
            0 AS UsePaletBarcodes,
            '' AS Address,
            0 AS UseAkciza,
            0 AS MulliriID,
            0 AS AllowPromotion,
            0 AS DownloadPartnersByDepartment,
            0 AS InsBy,
            0 AS IsDefaultDepartment
        UNION ALL
        SELECT * FROM chosen
        ORDER BY DepartmentName;
    END
    ELSE
    BEGIN
        ;WITH filtered AS
        (
            SELECT 
                d.DepartmentID,
                d.DepartmentName,
                d.Account,                                -- only once
                ISNULL(d.PriceMenuID,'')                 AS PriceMenuID,
                ISNULL(p.Name,'')                        AS PriceMenuName,
                ISNULL(d.CompanyID,'')                   AS CompanyID,
                ISNULL(c.Company,'')                     AS Company,
                ISNULL(d.StartCount,'')                  AS StartCount,
                ISNULL(d.EndCount,'')                    AS EndCount,
                ISNULL(d.LUB,0)                          AS LUB,
                ISNULL(d.LUD,'2000-01-01')               AS LUD,
                d.AllowNegative,
                ISNULL(d.LUN,0)                          AS LUN,
                d.Active,
                d.DepartmentTypeID,
                dt.DepartmentType,
                ISNULL(d.UsePaletBarcodes,0)             AS UsePaletBarcodes,
                d.Address,
                ISNULL(d.UseAkciza,0)                    AS UseAkciza,
                ISNULL(d.MulliriID,0)                    AS MulliriID,
                ISNULL(d.AllowPromotion,0)               AS AllowPromotion,
                ISNULL(d.DownloadPartnersByDepartment,0) AS DownloadPartnersByDepartment,
                d.InsBy,
                ISNULL(d.WooCommerceLocationID,0)        AS WooCommerceLocationID,
                CASE WHEN d.DepartmentID = c.DefaultDepID THEN 1 ELSE 0 END AS IsDefaultDepartment
            FROM dbo.tblDepartment AS d
            LEFT JOIN dbo.tblPriceMenu      AS p  ON p.ID  = d.PriceMenuID
            LEFT JOIN dbo.tblDepartmentType AS dt ON dt.ID = d.DepartmentTypeID
            LEFT JOIN dbo.tblCompany        AS c  ON c.ID  = d.CompanyID
            WHERE d.DepartmentID > 0
              -- ONLY search when @q is present
              AND (@q IS NOT NULL AND d.DepartmentName LIKE '%' + @q + '%')
        ),
        fallback AS
        (
            SELECT TOP (1)
                d.DepartmentID,
                d.DepartmentName,
                d.Account,                                -- only once
                ISNULL(d.PriceMenuID,'')                 AS PriceMenuID,
                ISNULL(p.Name,'')                        AS PriceMenuName,
                ISNULL(d.CompanyID,'')                   AS CompanyID,
                ISNULL(c.Company,'')                     AS Company,
                ISNULL(d.StartCount,'')                  AS StartCount,
                ISNULL(d.EndCount,'')                    AS EndCount,
                ISNULL(d.LUB,0)                          AS LUB,
                ISNULL(d.LUD,'2000-01-01')               AS LUD,
                d.AllowNegative,
                ISNULL(d.LUN,0)                          AS LUN,
                d.Active,
                d.DepartmentTypeID,
                dt.DepartmentType,
                ISNULL(d.UsePaletBarcodes,0)             AS UsePaletBarcodes,
                d.Address,
                ISNULL(d.UseAkciza,0)                    AS UseAkciza,
                ISNULL(d.MulliriID,0)                    AS MulliriID,
                ISNULL(d.AllowPromotion,0)               AS AllowPromotion,
                ISNULL(d.DownloadPartnersByDepartment,0) AS DownloadPartnersByDepartment,
                d.InsBy,
                ISNULL(d.WooCommerceLocationID,0)        AS WooCommerceLocationID,
                1 AS IsDefaultDepartment
            FROM dbo.tblDepartment AS d
            LEFT JOIN dbo.tblPriceMenu      AS p  ON p.ID  = d.PriceMenuID
            LEFT JOIN dbo.tblDepartmentType AS dt ON dt.ID = d.DepartmentTypeID
            LEFT JOIN dbo.tblCompany        AS c  ON c.ID  = d.CompanyID
            WHERE d.DepartmentID > 0
              AND d.DepartmentID = c.DefaultDepID
            ORDER BY ISNULL(c.[OrderBy], 0), d.DepartmentName
        ),
        chosen AS
        (
            SELECT * FROM filtered
            UNION ALL
            SELECT * FROM fallback
            WHERE NOT EXISTS (SELECT 1 FROM filtered)
        )
        SELECT * FROM chosen
        ORDER BY DepartmentName;
    END
END
GO