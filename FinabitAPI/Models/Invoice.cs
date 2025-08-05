using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finabit_API.Models
{
    public class Invoice
    {
        private int _mChannelID;
        private int _mCompanyID;
        private int _mDocPriceID;
        private string _mFirm;
        private DateTime _mInvoiceDate;
        private string _mInvoiceNo;

        private decimal _mInvoiceValue;
        private decimal _mPrice;
        private DateTime _mSHCHDate;
        private DateTime _mSHCHTime;
        private int _mStatusID;
        private decimal _mVAT;
        private int _OwnerID;
        private string _ChannelName;
        private string _ClientName;
        private string _DocPriceName;
        private string _Firm;
        private int _HomologationTypeID;
        private string _HomologationTypeName;
        private bool _Used;
        private int _InvoiceType;
        private string _Name;
        private Owner _Owner = new Owner();
        private int _Quantity;
        private DateTime? _SHCHDate;
        private string _SHCHTimeText;
        private string _VehicleTypeName;

        private int _ConfirmedPaidBy;
        private string _ConfirmedPaidName;

        private bool _IsEditedPlate;

        
        public int ChannelID
        {
            get
            {
                return this._mChannelID;
            }
            set
            {
                this._mChannelID = value;
            }
        }
        
        public string ChannelName
        {

            get
            {
                return this._ChannelName;
            }

            set
            {
                this._ChannelName = value;
            }
        }

        
        public string ClientName
        {

            get
            {
                return this._ClientName;
            }

            set
            {
                this._ClientName = value;
            }
        }

        
        public int CompanyID
        {
            get
            {
                return this._mCompanyID;
            }
            set
            {
                this._mCompanyID = value;
            }
        }

        
        public int DocPriceID
        {
            get
            {
                return this._mDocPriceID;
            }
            set
            {
                this._mDocPriceID = value;
            }
        }

        
        public string DocPriceName
        {

            get
            {
                return this._DocPriceName;
            }

            set
            {
                this._DocPriceName = value;
            }
        }

        
        public string Firm
        {

            get
            {
                return this._Firm;
            }

            set
            {
                this._Firm = value;
            }
        }

        
        public int HomologationTypeID
        {

            get
            {
                return this._HomologationTypeID;
            }

            set
            {
                this._HomologationTypeID = value;
            }
        }

        
        public string HomologationTypeName
        {

            get
            {
                return this._HomologationTypeName;
            }

            set
            {
                this._HomologationTypeName = value;
            }
        }

        
        public bool Used
        {

            get
            {
                return this._Used;
            }

            set
            {
                this._Used = value;
            }
        }

        
        public DateTime InvoiceDate
        {
            get
            {
                return this._mInvoiceDate;
            }
            set
            {
                this._mInvoiceDate = value;
            }
        }

        
        public string InvoiceNo
        {
            get
            {
                return this._mInvoiceNo;
            }
            set
            {
                this._mInvoiceNo = value;
            }
        }

        
        public int InvoiceType
        {

            get
            {
                return this._InvoiceType;
            }

            set
            {
                this._InvoiceType = value;
            }
        }

        
        public decimal InvoiceValue
        {
            get
            {
                return this._mInvoiceValue;
            }
            set
            {
                this._mInvoiceValue = value;
            }
        }

        
        public string Name
        {

            get
            {
                return this._Name;
            }

            set
            {
                this._Name = value;
            }
        }

        
        public Owner Owner
        {

            get
            {
                return this._Owner;
            }

            set
            {
                this._Owner = value;
            }
        }

        
        public int OwnerID
        {
            get
            {
                return this._OwnerID;
            }
            set
            {
                this._OwnerID = value;
            }
        }

        
        public decimal Price
        {
            get
            {
                return this._mPrice;
            }
            set
            {
                this._mPrice = value;
            }
        }

        
        public int Quantity
        {

            get
            {
                return this._Quantity;
            }

            set
            {
                this._Quantity = value;
            }
        }

        
        public DateTime? SHCHDate
        {

            get
            {
                return this._SHCHDate;
            }

            set
            {
                this._SHCHDate = value;
            }
        }

        
        public DateTime SHCHTime
        {
            get
            {
                return this._mSHCHTime;
            }
            set
            {
                this._mSHCHTime = value;
            }
        }

        
        public string SHCHTimeText
        {

            get
            {
                return this._SHCHTimeText;
            }

            set
            {
                this._SHCHTimeText = value;
            }
        }

        
        public int StatusID
        {
            get
            {
                return this._mStatusID;
            }
            set
            {
                this._mStatusID = value;
            }
        }


        
        public int ConfirmedPaidBy
        {
            get
            {
                return this._ConfirmedPaidBy;
            }
            set
            {
                this._ConfirmedPaidBy = value;
            }
        }

        
        public string ConfirmedPaidName
        {
            get
            {
                return this._ConfirmedPaidName;
            }
            set
            {
                this._ConfirmedPaidName = value;
            }
        }

        
        public decimal VAT
        {
            get
            {
                return this._mVAT;
            }
            set
            {
                this._mVAT = value;
            }
        }

        
        public string VehicleTypeName
        {

            get
            {
                return this._VehicleTypeName;
            }

            set
            {
                this._VehicleTypeName = value;
            }
        }

        
        public bool IsEditedPlate
        {
            get
            {
                return this._IsEditedPlate;
            }
            set
            {
                this._IsEditedPlate = value;
            }
        }

        
        public DocumentPrice DocumentPrice { get; set; }

        
        public string HomologTypeName { get; set; }

        
        public decimal FeePrice { get; set; }
        
        public decimal FeeVAT { get; set; }
        
        public decimal FeeTotalValue { get; set; }
        
        public decimal InvoiceTotalValue { get; set; }
        
        public int VehicleTypeID { get; set; }
        
        public int Active { get; set; }
        
        public string CompanyName { get; set; }
        
        public string Remarks { get; set; }
        
        public string InvoiceANo { get; set; }
        
        public bool Paid { get; set; }
        
        public bool PostYN { get; set; }

        
        public bool Loan { get; set; }

        
        public string PersonalNo { get; set; }

        
        public Nullable<DateTime> PaidDate { get; set; }

        
        public Nullable<DateTime> PaidTime { get; set; }

        
        public int ReturnID { get; set; }
        public bool Returned { get; set; }

        public int WeightType { get; set; }
        public bool OwnerUsedToday { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int VIN { get; set; }
        public int HomInvoiceID { get; set; }
        public bool AllowPrintDoc { get; set; }
        public int Color { get; set; }
        public int DossierID { get; set; }
        public int HomologTiporID { get; set; }

        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerMiddleName { get; set; }
        public string OwnerPersonalNo { get; set; }
        public string OwnerAddress { get; set; }
        public string OwnerPlace { get; set; }
        public DateTime? OwnerBirthDate { get; set; }
        public int PranuesiID { get; set; }
        public bool IsPeriodic { get; set; }
        public bool FromWeb { get; set; }
        public int VINProcesverbalID { get; set; }
        public bool AllowKTInsert { get; set; }
        public int ATestID { get; set; }
        public string Description { get;  set; }
        public int LUB { get;  set; }
        public int ErrorID { get; set; }
        public string ErrorDescription { get; set; }
        public int ID { get; set; }
    }
}