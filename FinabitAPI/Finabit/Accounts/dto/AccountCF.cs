using FinabitAPI.Core.Global.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Account.dto
{
    public class AccountCF : BaseClass
    {

        #region Class_Members


        /// Members

        private string _mAccountCFName = "";

        #endregion

        #region Class_Methods




        /// <summary>
        /// The AccountCFName field in the DB
        /// </summary>
        public string AccountCFName
        {
            get { return _mAccountCFName; }
            set { _mAccountCFName = value; }
        }


        #endregion
    }
}