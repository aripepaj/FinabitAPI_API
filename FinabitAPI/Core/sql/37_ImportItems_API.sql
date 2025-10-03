/****** Object:  StoredProcedure [dbo].[_ImportItems] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.[_ImportItems_API]', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.[_ImportItems_API] AS RETURN;');
GO

ALTER PROCEDURE [dbo].[_ImportItems_API]
    @ImportItems ImportItems READONLY,
    @NewID INT
AS
SET NOCOUNT ON;  -- NEW

-- NEW: keep track of the ItemIDs that get inserted now
DECLARE @NewItems TABLE (ItemID NVARCHAR(200) PRIMARY KEY);

-- original logic unchanged below
DELETE FROM dbo.tblTransactionsDetails WHERE TransactionID = @NewID;

DECLARE @userid INT,
        @IncomeAccount INT, 
        @ExpenseAccount INT, 
        @AssetAccount INT;

SELECT  @IncomeAccount = '7000', 
        @ExpenseAccount = '6000', 
        @AssetAccount = '1000'
FROM dbo.tblOptions;

SELECT TOP 1 @userid = UserID FROM dbo.tblUsers ORDER BY UserID;

---- njesite
INSERT INTO dbo.tblUnits
( UnitName, Active, InsDate, InsBy, LUN, LUD, LUB )
SELECT DISTINCT
       UnitName,
       1,
       GETDATE(),
       @userid,
       0,
       GETDATE(),
       @userid
FROM @ImportItems 
WHERE LEN(UnitName) > 0
  AND UnitName NOT IN (SELECT UnitName FROM dbo.tblUnits)
ORDER BY UnitName;

--grupet
INSERT INTO dbo.tblItemGroup
( ItemGroup, Active, InsBy, InsDate, LUN, LUD, LUB, IncomeAccount, ExpenseAccount, AssetAccount, Color )
SELECT DISTINCT 
       CategoryName,
       1,
       @userid,
       GETDATE(),
       0,
       GETDATE(),
       @userid,
       @IncomeAccount, 
       @ExpenseAccount, 
       @AssetAccount, 
       '255,255,255,255'
FROM @ImportItems 
WHERE CategoryName IS NOT NULL
  AND CategoryName NOT IN (SELECT ItemGroup FROM dbo.tblItemGroup)
ORDER BY CategoryName;

--- artikujt
INSERT INTO dbo.tblItems
(
    ItemID, 
    ItemName, 
    ItemType, 
    ItemGroupID, 
    UnitID, 
    IncomeAccount, 
    ExpenseAccount, 
    AssetAccount, 
    QuantityOnHand, 
    QuantityOnPO, 
    QuantityOnSO, 
    MinimumQuantity, 
    OrderQuantity, 
    ActualPrice, 
    PurchasePrice, 
    SalesPrice,  
    Taxable, 
    Active, 
    Dogana, 
    InsBy, 
    InsDate, 
    LUN, 
    LUD, 
    LUB, 
    Akciza,
    IsFixedAsset, 
    AutomaticRealisation,
    PreferredVendor,
    DamagePercent,
    Color,
    VATValue,
    DoganaValue,
    AkcizaValue
)
OUTPUT inserted.ItemID INTO @NewItems(ItemID)   -- NEW
SELECT 
    a.ItemID,
    a.ItemName, 
    1, 
    ig.ID, 
    UnitID, 
    IncomeAccount, 
    ExpenseAccount, 
    AssetAccount, 
    0, 
    0, 
    0, 
    0, 
    0, 
    0, 
    0, 
    0,  
    1, 
    1, 
    0, 
    @userid,
    GETDATE(),
    0,
    GETDATE(),
    @userid, 
    0,
    0, 
    0,
    0,
    0,
    '255,255,255,255',
    16,
    10,
    0
FROM @ImportItems a
INNER JOIN dbo.tblUnits u       ON u.UnitName   = a.UnitName
INNER JOIN dbo.tblItemGroup ig  ON ig.ItemGroup = a.CategoryName
WHERE a.ItemID NOT IN (SELECT ItemID FROM dbo.tblItems);

INSERT INTO dbo.tblTransactionsDetails
(
  TransactionID,
  DetailsType,
  ItemID,
  ItemName,
  Account,
  Quantity,
  Price,
  Discount,
  PriceWithDiscount,
  Value,
  ProjectID,
  PaymentID,
  PaymentValue,
  PurchasedValue,
  StockQuantity,
  InsBy,
  InsDate,
  LUB,
  LUN,
  LUD,
  CostPrice,
  InvoiceID,
  SalesPrice,
  VATPrice,
  ItemAssemblyID,
  Damaged,
  ItemTransferFrom,
  DepartmentID,
  InternalDepartmentID,
  TransactionDate,
  TransactionTypeID,
  ItemTYpe
)
SELECT  
    @NewID,
    1,
    td.ItemID,
    td.ItemName,
    NULL,
    ISNULL(td.Quantity,0),
    ISNULL(td.Price, 0),
    0,
    ISNULL(td.Price, 0),
    ISNULL(td.Price, 0) * ISNULL(td.Quantity,0),
    NULL,
    NULL,
    0,
    0,
    0,
    @userid,
    GETDATE(),
    @userid,
    0,
    GETDATE(),
    ISNULL(td.Price, 0),
    NULL,
    ISNULL(td.Price, 0),
    ISNULL(td.Price, 0),
    NULL,
    NULL,
    NULL,
    DepartmentID,
    InternalDepartmentID,
    TransactionDate,
    TransactionTypeID,
    1
FROM @ImportItems td
INNER JOIN dbo.tblTransactions t ON t.ID = @NewID;

-- NEW: return all columns for items that were newly inserted in THIS call
SELECT i.*
FROM dbo.tblItems AS i
JOIN @NewItems   AS n ON n.ItemID = i.ItemID
ORDER BY i.ItemID;
GO
