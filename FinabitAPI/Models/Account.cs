using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class Account :BaseClass
    {
        #region Class_Methods

        public Account()
        {
            AccountCode = "";
            AccountParent = "";
            AccountLevel = 0;
            AccountStatus = "";
            AccountDescription = "";
            AccountSubGroupID = 0;
            AccountCFCategoryID = 0;
            Active = false;
            AccountDescription_2 = "";

        }

        private AccountCF mAccountCF = new AccountCF();
        private AccountGroup mAccountGroup = new AccountGroup();
        private AccountSubGroup mAccountSubGroup = new AccountSubGroup();
        private Bank mBank = new Bank();
        /// Methods
        /// <summary>
        /// The Account field in the DB
        /// </summary>
        public string AccountCode
        {
            get;
            set;
        }

        /// <summary>
        /// The AccountParent field in the DB
        /// </summary>
        /// 
        public string AccountParent
        {
            get;
            set;
        }

        /// <summary>
        /// The AccountLevel field in the DB
        /// </summary>
        public int AccountLevel
        {
            get;
            set;
        }

        /// <summary>
        /// The AccountStatus field in the DB
        /// </summary>
        public string AccountStatus
        {
            get;
            set;
        }

        /// <summary>
        /// The AccountDescription field in the DB
        /// </summary>
        public string AccountDescription
        {
            get;
            set;
        }

        /// <summary>
        /// The AccountSubGroupID field in the DB
        /// </summary>
        public int AccountSubGroupID
        {
            get;
            set;
        }

        /// <summary>
        /// The AccountCFCategoryID field in the DB
        /// </summary>
        public int AccountCFCategoryID
        {
            get;
            set;
        }

        /// <summary>
        /// The Active field in the DB
        /// </summary>
        public bool Active
        {
            get;
            set;
        }

        public AccountCF AccountCF
        {
            get { return mAccountCF; }
            set { mAccountCF = value; }
        }

        public AccountGroup AccountGroup
        {
            get { return mAccountGroup; }
            set { mAccountGroup = value; }
        }

        public AccountSubGroup AccountSubGroup
        {
            get { return mAccountSubGroup; }
            set { mAccountSubGroup = value; }
        }

        public Bank Bank
        {
            get { return mBank; }
            set { mBank = value; }
        }

        /// <summary>
        /// The AccountDescription_2 field in the DB
        /// </summary>
        public string AccountDescription_2
        {
            get;
            set;
        }

        //public DataTable Users { get; set; }

        #endregion
    }
}