IF NOT EXISTS (
    SELECT 1
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'dbo.spTransactionWithDetails_API')
      AND type IN (N'P', N'PC')
)
BEGIN
    EXEC ('CREATE PROCEDURE dbo.spTransactionWithDetails_API AS BEGIN SET NOCOUNT ON; RETURN; END');
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE dbo.spTransactionWithDetails_API
(
    @TransactionID INT
)
AS
SET NOCOUNT ON;

-- 1) Header
SELECT *
FROM dbo.tblTransactions
WHERE ID = @TransactionID;

-- 2) Details (your query, just switch the WHERE to TransactionID)
SELECT 
    td.[ID],
    td.[TransactionID],
    [DetailsType],
    td.[ItemID],
    td.ItemID as Item,
    ''  as ItemDescription,
    td.[ItemName],
    [Contracts],
    ISNULL(td.Account,'') as Account,
    ISNULL(td.CostPrice,0) as CostPrice,
    td.Quantity,
    ISNULL([Price],0) as Price,
    0.00 as RABAT,
    0.00 as PriceWithRABAT,
    [Discount],
    [PriceWithDiscount],
    td.Quantity,
    ISNULL(td.Value,0) as Value,
    td.ProjectID,
    ISNULL([PaymentID],0) as PaymentID,
    ISNULL([PaymentValue],0) as PaymentValue,
    ISNULL([PurchasedValue],0) as PurchasedValue,
    ISNULL([StockQuantity],0) AS StockQuantity,
    ISNULL([ItemTransferFrom],'') as [ItemTransferFrom],
    ISNULL(td.[InsBy],0) as InsBy,
    ISNULL(td.[InsDate],'2000-1-1') as InsDate,
    ISNULL(td.[LUB],0) as LUB,
    ISNULL(td.[LUN],0) as LUN,
    ISNULL(td.[LUD],'2000-1-1') AS LUD,
    '' as UnitDescription,
    '' as HasSerials,
    CAST(0 as money) as SalesMargin,
    td.SalesPrice,
    td.SalesPrice2,
    VATPrice,
    VATPrice as OriginVATPrice,
    0 as Taxable,
    0.00 as SalesValue,
    0 as ItemStatus,
    0.00 as VATPriceValue,
    IsNull(OriginalPrice,0) as OriginalPrice,
    Contracts,
    0 as LocationID,
    0.00 as IM7Price,
    0.00 as LeftQuantity,
    0.00 as RemovedQuantity,
    0.00 as OverValue,
    0.00 as DoganaValue,
    0.00 as VATPercent,
    0.00 as AkcizaValue,
    u.UnitID,
    td.Coefficient,
    i.barcode1  as Barcode,
    u.UnitName,
    0 as ID1,
    0 as Coef_Quantity,
    0 as Coef_Price,
    td.VATValue ,
    0 as Coefficient2,
    CAST(0 as decimal(23,8)) as Quantity2,
    td.StockLocationID,
    sl.StockLocationName,
    ISNULL(td.StockPrice,0) as CmimiOrigjinal,
    i.ID as ArticleID,
    (select UnitName from tblUnits u where u.UnitID=i.UnitID) as ItemUnitName,
    0 As Paleta,
    1.00 as ItemCoefficient,
    td.ExpirationDate,
    ISNULL(td.CompensationID,0) as CompensationID,
    td.Transport * td.Quantity as Transport, 
    CASE WHEN ISNULL(i.PDAItemName,'') = '' THEN td.ItemName else i.PDAItemName end as PDAItemName,
    ISNULL(td.salesmargin,0.00) as Marzhina,
    0.00 as CmimiShitjesMeMarzhine,
    0 as GroupVipValue,
    ISNULL(td.PriceFactor,0) as PriceFactor,
    td.Transport  as Transport,
    td.Quantity  *   ISNULL(IM7Price,0)  as InvoiceValue,
    CAST(0.00 as decimal(23,8)) as SalesMargin2,
    td.ItemType,
    '' as ItemTypedescription,
    td.Discount2 as RABAT2,
    ISNULL(td.AccountCode,'') AccountCode,
    ISNULL(PriceWithoutVATUse,0) PriceWithoutVATUse,
    i.Coef_Quantity as ItemCoef_Quantity,
    0 as LastBSValue,
    0 as CurrentBSValue,
    ISNULL(td.SubOrderID,0) as SubOrderID,
    ISNULL(td.SalesPrice3,0) as SalesPrice3,
    0 as Hide,
    ISNULL(td.AssetID,0) as AssetID,
    ISNULL(i.IsService,0) as IsService,
    ISNULL(i.IsInAction,0) as IsInAction,
    ISNULL(SubOrderReceived,0) as SubOrderReceived,
    ISNULL(td.Dim_1,0) as Dim_1,
    ISNULL(td.Dim_2,0) as Dim_2,
    ISNULL(td.Dim_3,0) as Dim_3,
    ISNULL(td.DepartmentID,0) as DepartmentID,
    0.00 as SalesValueWithoutVAT,
    ISNULL(td.RowOrder,0) as RowOrder,
    ISNULL(s.Number,'') as SerialNumber,
    ISNULL(td.GuaranteeDate,'2001-01-01') as GuaranteeDate,
    CAST(0.00 as decimal(23,8)) as SalesMargin3,
    ISNULL(POSProductionItemID,0) as POSProductionItemID,
    ISNULL(td.TransactionDate, GETDATE()) as TransactionDate,
    ISNULL(td.PositionNumber,'') as PositionNumber,
    ISNULL(i.PLU,'') as PLU,
    ISNULL(td.TargetPlanID,0) as TargetPlanID,
    CASE WHEN Discount = 100 THEN 0 ELSE VATPrice*(100/(100 - td.Discount)) END as VATPriceWithOutRABAT,
    CASE WHEN Discount = 100 THEN 0 ELSE td.Quantity * VATPrice*(100/(100 - td.Discount)) END as AllValueWithOutRabat,
    ISNULL(td.Memo,'') as Memo,
    ISNULL(st.MinimalPrice,0) as MinimalPrice,
    ISNULL(i.Origin,'') as Origin,
    ISNULL(td.ProductionSerialaNumber,'') as ProductionSerialaNumber,
    ISNULL(td.Quantity_Lot,0) as Quantity_Lot,
    ISNULL(td.KM,0) as KM,
    ISNULL(td.RBOOKID,0) as RBOOKID,
    ISNULL(td.PriceMenuID,0) as PriceMenuID,
    i.CustomField1,
    i.CustomField2,
    ISNULL(td.TaxAtSource,0) as TaxAtSource,
    CASE WHEN ISNULL(i.Coefficient4,0) = 0 THEN 1 ELSE ISNULL(i.Coefficient4,0) END as Coefficient4,
    td.Discount3 as RABAT3,
    ISNULL(st.Maximum,0) as Maximum,
    0  as StockQuantity2,
    0 as RemainedQuantity,
    i.CustomField3,
    i.barcode2 as Barcode2,
    pr.ProjectName,
    td.Quantity/ CASE WHEN ISNULL(i.Weight,0)=0 THEN 1 ELSE i.Weight END as QuantityBruto,
    i.CustomField4,
    i.CustomField5,
    i.CustomField6,
    ISNULL(td.Memo2,'') as 	Memo2,
    ISNULL(i.NettoBruttoWeight, 0) as NettoBruttoWeight,
    ISNULL(i.BrutoWeight, 0) as BrutoWeight,
    ISNULL(transactions.NumriPaletave, 0) as NumriPaletave,
    ISNULL(transactions.PeshaPaletes, 0) as PeshaPaletes,
    ISNULL(transactions.PeshaKartonit, 0) as PeshaKartonit,
    ISNULL(transactions.PlateNo, 0) as PlateNo,
    ISNULL(transactions.TermsOfDelivery, '') as TermsOfDelivery,
    ISNULL(transactions.CommodityCode, '') as CommodityCode,
    ISNULL(transactions.DeliveryMethod, '') as DeliveryMethod,
    ISNULL(transactions.ShipmentTerms, '') as ShipmentTerms,
    ISNULL(transactions.CustOrderNo, '') as CustOrderNo,
    ISNULL(p.Address, 0) as Address,
    ISNULL(p.ShippAddress, 0) as ShippAddress,
    CAST(i.Coef_Quantity as decimal(23,8)) as Coefficient3,
    ISNULL(transactions.Reference, '') as Reference,
    ISNULL(transactions.SealNo,'') as SealNo,
    ISNULL(itd.ProductionSerialNumber,td.ProductionSerialaNumber) ProductionSerialNumber_det,
    ISNULL(itd.ExpirationDate,td.ExpirationDate) as ExpirationDate_det,
    CASE WHEN ISNULL(td.VatValue,0) = 18 THEN ISNULL(td.Value,0) ELSE 0 END as VleraPaTVSH18,
    CASE WHEN ISNULL(td.VATValue,0) = 8  THEN ISNULL(td.Value,0) ELSE 0 END as VleraPaTVSH8,
    ISNULL(prodh.Emri,'') as Prodhuesi,
    ISNULL(ig.ItemGroup,'') as ItemGroup
FROM  [dbo].[tblTransactionsDetails] td
LEFT JOIN tblUnits u On u.UnitID = td.UnitID
LEFT JOIN tblStockLocation sl on sl.ID = td.StockLocationID
LEFT JOIN tblItems i on i.ItemID = td.ItemID
LEFT JOIN tblItemGroup ig on ig.ID = i.ItemGroupId
LEFT JOIN tblSerials s on s.TransactionID = td.TransactionID AND s.ItemID = td.ItemID
LEFT JOIN tblStocks st on st.ItemID = td.ItemID AND st.DepartmentID = td.DepartmentID
LEFT JOIN tblProjects pr on pr.ProjectID = td.ProjectID
LEFT JOIN tblTransactions transactions on transactions.ID = td.TransactionID
LEFT JOIN tblPartners p on p.PartnerID = transactions.PartnerID
LEFT JOIN tblItemTrackingDetails itd on itd.DetailsID = td.ID
LEFT JOIN tblProdhuesi prodh on prodh.ID = i.ProdhuesiID
WHERE td.TransactionID = @TransactionID;