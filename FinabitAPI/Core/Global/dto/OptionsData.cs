namespace FinabitAPI.Core.Global.dto
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public class OptionsData
    {
        public static int       ID { get; set; }
        public static int       POSPartnerID { get; set; }
        public static bool      AllTables { get; set; }
        public static int       NrCopies { get; set; }
        public static string    TerminCashAccount { get; set; }
        public static string    EmployeeCashAccount { get; set; }
        public static bool      GetFirstDescription { get; set; }
        public static string    EmployeeAdvanceAccount { get; set; }
        public static string    PayablesVATAccount { get; set; }
        public static string    SalesVATAccount { get; set; }
        public static string    ItemPendingAccount { get; set; }
        public static int       DUDPartner { get; set; }
        public static string    AkcizaPartner { get; set; }
        public static string    VATPartner { get; set; }
        public static string    FiscalPrinter { get; set; }
        public static string    CashAccount { get; set; }
        public static string    FINAPartnersAccount { get; set; }
        public static string    FeeAccount { get; set; }
        public static string    AdvanceAccount { get; set; }
        public static int       POSVAT { get; set; }
        public static bool      WorksWithRFID { get; set; }
        public static bool      UseOldItems { get; set; }
        public static bool      LogOffAfterPOS { get; set; }
        public static string    DoganaTemporary { get; set; }
        public static string    TransportTemporary { get; set; }
        public static string    AkcizaTemporary { get; set; }
        public static string    OverAsLiabilityAccount { get; set; }
        public static string    CustomsStockAccount { get; set; }

        //public static string    Printer1 { get; set; }
        //public static string    Printer2 { get; set; }
        public static bool      ProposeVAT { get; set; }
        public static bool      UseDoublePartners { get; set; }
        public static bool      UseRegularInvoice { get; set; }
        public static int     NumberOfCopy1 { get; set; }

        public static int ErrorID { get; set; }
        public static string ErrorDescription { get; set; }
        public static string PayableAccount { get; set; }
        public static string ReceivableAccount { get; set; }
        public static bool ShowTimeAtPOSInvoice { get; set; }
        public static bool AutomaticallyCalculatePrice { get; set; }

        public static bool ShowWages { get; set; }
        public static bool ShowFixedAssets { get; set; }
        public static bool ShowHotel { get; set; }

        public static bool PrintFiscalInvoice { get; set; }
        public static bool PrintFiscalAlways { get; set; }
        public static int TranNoType { get; set; }

        public static string GroupIncomeAccount { get; set; }
        public static string GroupExpenseAccount { get; set; }
        public static string GroupAssetAccount { get; set; }
        public static decimal Margin { get; set; }

        public static int DecimalNo { get; set; }
        public static bool MultipleCompany { get; set; }
        public static string MarketColor { get; set; }
        public static string IPService { get; set; }
        public static bool SingleTermin { get; set; }
        public static bool AllowVATChange { get; set; }
        public static bool SalesPriceWithVAT { get; set; }
        public static bool UseRestriction { get; set; }
        public static DateTime RestrictChangeUntil { get; set; }
        public static string ExpenseAccountOnPOS { get; set; }
        public static int HDepartmentID { get; set; }

        public static bool UseAnotherImportForm { get; set; }
        public static bool PrintFiscalAfterEachOrder { get; set; }
        public static bool UseSubOrders { get; set; }
        public static bool PrintTranIDInFiscal { get; set; }
        public static bool UseDoubleNumberInSales { get; set; }
        public static bool ShowPaletsInMobile { get; set; }

        public static bool GjeneroNotenKreditore { get; set; }

    }
}


