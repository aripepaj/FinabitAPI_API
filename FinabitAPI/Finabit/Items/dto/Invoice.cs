using FinabitAPI.Core.Global.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Items.dto
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
                return _mChannelID;
            }
            set
            {
                _mChannelID = value;
            }
        }
        
        public string ChannelName
        {

            get
            {
                return _ChannelName;
            }

            set
            {
                _ChannelName = value;
            }
        }

        
        public string ClientName
        {

            get
            {
                return _ClientName;
            }

            set
            {
                _ClientName = value;
            }
        }

        
        public int CompanyID
        {
            get
            {
                return _mCompanyID;
            }
            set
            {
                _mCompanyID = value;
            }
        }

        
        public int DocPriceID
        {
            get
            {
                return _mDocPriceID;
            }
            set
            {
                _mDocPriceID = value;
            }
        }

        
        public string DocPriceName
        {

            get
            {
                return _DocPriceName;
            }

            set
            {
                _DocPriceName = value;
            }
        }

        
        public string Firm
        {

            get
            {
                return _Firm;
            }

            set
            {
                _Firm = value;
            }
        }

        
        public int HomologationTypeID
        {

            get
            {
                return _HomologationTypeID;
            }

            set
            {
                _HomologationTypeID = value;
            }
        }

        
        public string HomologationTypeName
        {

            get
            {
                return _HomologationTypeName;
            }

            set
            {
                _HomologationTypeName = value;
            }
        }

        
        public bool Used
        {

            get
            {
                return _Used;
            }

            set
            {
                _Used = value;
            }
        }

        
        public DateTime InvoiceDate
        {
            get
            {
                return _mInvoiceDate;
            }
            set
            {
                _mInvoiceDate = value;
            }
        }

        
        public string InvoiceNo
        {
            get
            {
                return _mInvoiceNo;
            }
            set
            {
                _mInvoiceNo = value;
            }
        }

        
        public int InvoiceType
        {

            get
            {
                return _InvoiceType;
            }

            set
            {
                _InvoiceType = value;
            }
        }

        
        public decimal InvoiceValue
        {
            get
            {
                return _mInvoiceValue;
            }
            set
            {
                _mInvoiceValue = value;
            }
        }

        
        public string Name
        {

            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        
        public Owner Owner
        {

            get
            {
                return _Owner;
            }

            set
            {
                _Owner = value;
            }
        }

        
        public int OwnerID
        {
            get
            {
                return _OwnerID;
            }
            set
            {
                _OwnerID = value;
            }
        }

        
        public decimal Price
        {
            get
            {
                return _mPrice;
            }
            set
            {
                _mPrice = value;
            }
        }

        
        public int Quantity
        {

            get
            {
                return _Quantity;
            }

            set
            {
                _Quantity = value;
            }
        }

        
        public DateTime? SHCHDate
        {

            get
            {
                return _SHCHDate;
            }

            set
            {
                _SHCHDate = value;
            }
        }

        
        public DateTime SHCHTime
        {
            get
            {
                return _mSHCHTime;
            }
            set
            {
                _mSHCHTime = value;
            }
        }

        
        public string SHCHTimeText
        {

            get
            {
                return _SHCHTimeText;
            }

            set
            {
                _SHCHTimeText = value;
            }
        }

        
        public int StatusID
        {
            get
            {
                return _mStatusID;
            }
            set
            {
                _mStatusID = value;
            }
        }


        
        public int ConfirmedPaidBy
        {
            get
            {
                return _ConfirmedPaidBy;
            }
            set
            {
                _ConfirmedPaidBy = value;
            }
        }

        
        public string ConfirmedPaidName
        {
            get
            {
                return _ConfirmedPaidName;
            }
            set
            {
                _ConfirmedPaidName = value;
            }
        }

        
        public decimal VAT
        {
            get
            {
                return _mVAT;
            }
            set
            {
                _mVAT = value;
            }
        }

        
        public string VehicleTypeName
        {

            get
            {
                return _VehicleTypeName;
            }

            set
            {
                _VehicleTypeName = value;
            }
        }

        
        public bool IsEditedPlate
        {
            get
            {
                return _IsEditedPlate;
            }
            set
            {
                _IsEditedPlate = value;
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

        
        public DateTime? PaidDate { get; set; }

        
        public DateTime? PaidTime { get; set; }

        
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