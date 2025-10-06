using FinabitAPI.Core.Global.dto;
using FinabitAPI.Finabit.Partner.dto;

namespace FinabitAPI
{
    public class Partner : BaseClass
    {
        private string _mPartnerName = "";
        private PartnersType _mPartnerType = new PartnersType();
        private string _mContactPerson = "";
        private State _mState = new State();
        private Place _mPlace = new Place();
        private string _mAddress = "";
        private string _mTel1 = "";
        private string _mTel2 = "";
        private string _mFax = "";
        private string _mEmail = "";
        private string _mWebSite = "";
        private Account _mAccount = new Account();
        private string _mBusinessNo = "";
        private string _mBankAccount = "";


        public string PartnerName
        {
            get { return _mPartnerName; }
            set { _mPartnerName = value; }
        }


        public PartnersType PartnerType
        {
            get { return _mPartnerType; }
            set { _mPartnerType = value; }
        }

        public string ContactPerson
        {
            get { return _mContactPerson; }
            set { _mContactPerson = value; }
        }


        public State State
        {
            get { return _mState; }
            set { _mState = value; }
        }

        public int RegionID { get; set; }

        public Account Account
        {
            get { return _mAccount; }
            set { _mAccount = value; }
        }

        public Place Place
        {
            get { return _mPlace; }
            set { _mPlace = value; }
        }

        public string Address
        {
            get { return _mAddress; }
            set { _mAddress = value; }
        }

        public string Tel1
        {
            get { return _mTel1; }
            set { _mTel1 = value; }
        }
        public string Tel2
        {
            get { return _mTel2; }
            set { _mTel2 = value; }
        }


        public string Fax
        {
            get { return _mFax; }
            set { _mFax = value; }
        }


        public string Email
        {
            get { return _mEmail; }
            set { _mEmail = value; }
        }

        public string WebSite
        {
            get { return _mWebSite; }
            set { _mWebSite = value; }
        }

        public string BusinessNo
        {
            get { return _mBusinessNo; }
            set { _mBusinessNo = value; }
        }

        public string BankAccount
        {
            get { return _mBankAccount; }
            set { _mBankAccount = value; }
        }

        public decimal DiscountPercent { get; set; }
        public string PIN { get; set; }

        public string ItemID { get; set; }

        public int PriceMenuID { get; set; }

        public int DueDays { get; set; }
        public decimal DueValueMaximum { get; set; }
        public string ContractNo { get; set; }
        public int PartnerCategoryID { get; set; }
        public string Evaluation { get; set; }
        public string PartnerGroup { get; set; }
        public string RealBusinessNo { get; set; }
        public string ICCOM { get; set; }
        public int DiscountLevel { get; set; }
        public string VATNO { get; set; }
        public bool AllowSaleWithMaxDue { get; set; }
        public decimal PartnerBalance { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int RouteOrderID { get; set; }
        public bool HasVAT { get; set; }
        public string OwnerName { get; set; }

        public byte[] PartnerPicture { get; set; }

        public Partner()
        {
            DiscountPercent = 0;
            PIN = "";
            ItemID = "";
            PriceMenuID = 0;
            DueDays = 0;
            DueValueMaximum = 0;
            ContractNo = "";
            PartnerCategoryID = 0;
            Evaluation = "";
            PartnerGroup = "";
            RealBusinessNo = "";
            ICCOM = "";
            DiscountLevel = 0;
            VATNO = "";
            AllowSaleWithMaxDue = true;
            PartnerBalance = 0;
            Longitude = 0;
            Latitude = 0;
            RouteOrderID = 0;
            HasVAT = true;
            OwnerName = "";

        }
    }
}