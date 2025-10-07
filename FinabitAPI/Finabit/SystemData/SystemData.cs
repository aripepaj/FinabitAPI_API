using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Finabit.SystemData
{
    public class SystemData : BaseClass
    {
        private string _mComName;
        private string _mComBusinessNo;
        private string _mComFiscalNo;
        private string _mComVATNo;
        private string _mComPhone;
        private string _mComFax;
        private string _mComAddress;
        private string _mComBankName;
        private string _mComBankAccount;
        private string _mComBankName2;
        private string _mComBankAccount2;
        private string _mComBankName3;
        private string _mComBankAccount3;
        private string _mComBankName4;
        private string _mComBankAccount4; 
        private string _mComEmail;
        private string _mComWebAddress;
        private byte[] _mComLogo;
        private decimal _mVATPrc;

        public string EmailFrom { get; set; }
        public string EmailBody { get; set; }
        public string EmailSMTP { get; set; }
        public string EmailPassword { get; set; }
        public int EmailPort { get; set; }
        public bool EmailEnableSSL { get; set; }
        public string ReceivingBank { get; set; }
        public string ReceivingCustomerAccount { get; set; }
        public string ReceivingCustomerName { get; set; }
        public string Mod36 { get; set; }
        public int BussinesType { get; set; }
        public string APIToken { get; set; }
        public string NrIdentifikues { get; set; }
        public byte[] ReportImage_BAKI { get; set; }
        public string DecryptKey { get; set; }
        public string DisplayEmailName { get; set; }


        public string ComAddress
        {
            get
            {
                return this._mComAddress;
            }
            set
            {
                this._mComAddress = value;
            }
        }

        public string ComBankAccount
        {
            get
            {
                return this._mComBankAccount;
            }
            set
            {
                this._mComBankAccount = value;
            }
        }

        public string ComBankAccount2
        {
            get
            {
                return this._mComBankAccount2;
            }
            set
            {
                this._mComBankAccount2 = value;
            }
        }

        public string ComBankAccount3
        {
            get
            {
                return this._mComBankAccount3;
            }
            set
            {
                this._mComBankAccount3 = value;
            }
        }
        public string ComBankAccount4
        {
            get
            {
                return this._mComBankAccount4;
            }
            set
            {
                this._mComBankAccount4 = value;
            }
        }

        public string ComBankName
        {
            get
            {
                return this._mComBankName;
            }
            set
            {
                this._mComBankName = value;
            }
        }

        public string ComBankName2
        {
            get
            {
                return this._mComBankName2;
            }
            set
            {
                this._mComBankName2 = value;
            }
        }

        public string ComBankName3
        {
            get
            {
                return this._mComBankName3;
            }
            set
            {
                this._mComBankName3 = value;
            }
        }
        public string ComBankName4
        {
            get
            {
                return this._mComBankName4;
            }
            set
            {
                this._mComBankName4 = value;
            }
        }

        public string ComBusinessNo
        {
            get
            {
                return this._mComBusinessNo;
            }
            set
            {
                this._mComBusinessNo = value;
            }
        }

        public string ComFiscalNo
        {
            get
            {
                return this._mComFiscalNo;
            }
            set
            {
                this._mComFiscalNo = value;
            }
        }

        public string ComVATNo
        {
            get
            {
                return this._mComVATNo;
            }
            set
            {
                this._mComVATNo = value;
            }
        }

        public string ComEmail
        {
            get
            {
                return this._mComEmail;
            }
            set
            {
                this._mComEmail = value;
            }
        }
        public string ComWebAddress
        {
            get
            {
                return this._mComWebAddress;
            }
            set
            {
                this._mComWebAddress = value;
            }
        }
        public string ComFax
        {
            get
            {
                return this._mComFax;
            }
            set
            {
                this._mComFax = value;
            }
        }

        public byte[] ComLogo
        {
            get
            {
                return this._mComLogo;
            }
            set
            {
                this._mComLogo = value;
            }
        }

        public string ComName
        {
            get
            {
                return this._mComName;
            }
            set
            {
                this._mComName = value;
            }
        }

        public string ComPhone
        {
            get
            {
                return this._mComPhone;
            }
            set
            {
                this._mComPhone = value;
            }
        }

        public decimal VATPrc
        {
            get
            {
                return this._mVATPrc;
            }
            set
            {
                this._mVATPrc = value;
            }
        }

        public int DUDPartner { get; set; }
        public string VATPartner { get; set; }
        public string AkcizaPartner { get; set; }
        public string PayablesVATAccount { get; set; }

        public bool ShowLastUpdateInfo { get; set; }
        public string InfoText { get; set; }

        //public string ReceivingBank { get; set; }
        //public string ReceivingCustomerAccount { get; set; }
        //public string ReceivingCustomerName { get; set; }
        //public string Mod36 { get; set; }
    }
}