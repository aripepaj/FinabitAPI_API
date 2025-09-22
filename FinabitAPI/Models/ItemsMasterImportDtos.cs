// File: Finabit_API/Models/ItemsMasterImportDtos.cs
using System;                    // <-- needed for DateTime
using System.Collections.Generic;

namespace Finabit_API.Models
{
    public sealed class ImportItemRow
    {
        public string ItemID { get; set; } = "";
        public string ItemName { get; set; } = "";

        // renamed to match TVP
        public string CategoryName { get; set; } = "";   // was Category
        public string UnitName { get; set; } = "";       // was Unit1

        // NEW – used by proc when present in TVP
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int DepartmentID { get; set; }
        public int InternalDepartmentID { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int TransactionTypeID { get; set; }

        // Optional fields (left null if your TVP doesn’t contain them)
        public string Unit2 { get; set; }
        public decimal? Coef_Quantity { get; set; }
        public string ItemType { get; set; }
        public string IncomeAccount { get; set; }
        public string ExpenseAccount { get; set; }
        public string AssetAccount { get; set; }
        public decimal? MinimumQuantity { get; set; }
        public decimal? SalesPrice { get; set; }
        public string TaxValue { get; set; }
        public string Origin { get; set; }
        public string SubGroupID { get; set; }
        public string Barcode1 { get; set; }
        public string Barcode3 { get; set; }
        public decimal? SalesPrice2 { get; set; }
        public decimal? SalesPrice3 { get; set; }
        public decimal? Coefficient { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }
        public string CustomField6 { get; set; }
        public string CustomFields7 { get; set; }
        public string CustomFields8 { get; set; }
        public string PLU { get; set; }
    }

    public sealed class ImportAssemblyRow
    {
        public string ItemAssemblyID { get; set; } = ""; // parent ItemID
        public string ItemID { get; set; } = "";         // component ItemID
        public decimal Quantity { get; set; }            // required
    }

    public sealed class ImportItemsMasterRequest
    {
        public List<ImportItemRow> Items { get; set; } = new();
        public List<ImportAssemblyRow> Assemblies { get; set; } = new();
    }

    public sealed class ImportItemsMasterResponse
    {
        public int Inserted { get; set; }
        public string Error { get; set; }
    }
}
