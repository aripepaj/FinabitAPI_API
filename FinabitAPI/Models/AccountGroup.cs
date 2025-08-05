using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class AccountGroup : BaseClass
    {

        #region Class_Members


        /// Members

        private string _mAccountGroupName = "";

        #endregion

        #region Class_Methods




        /// <summary>
        /// The AccountGroupName field in the DB
        /// </summary>
        public string AccountGroupName
        {
            get { return _mAccountGroupName; }
            set { _mAccountGroupName = value; }
        }


        #endregion
    }
}