using FinabitAPI.Core.Global.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Account.dto
{
    public class AccountSubGroup : BaseClass
    {

        #region Class_Members


        /// Members
        private int _mAccountSubGroupID = 0;
        private string _mAccountSubGroupName = "";
        private string _mAccountSubGroupName_1 = "";
        private string _mAccountSubGroupName_2 = "";
        private int _mAccountGroup = 0;

        #endregion

        #region Class_Methods


        /// Methods
        /// <summary>
        /// The AccountSubGroupID field in the DB
        /// </summary>
        public int AccountSubGroupID
        {
            get { return _mAccountSubGroupID; }
            set { _mAccountSubGroupID = value; }
        }

        /// <summary>
        /// The AccountSubGroupName field in the DB
        /// </summary>
        public string AccountSubGroupName
        {
            get { return _mAccountSubGroupName; }
            set { _mAccountSubGroupName = value; }
        }

        /// <summary>
        /// The AccountSubGroupName_1 field in the DB
        /// </summary>
        public string AccountSubGroupName_1
        {
            get { return _mAccountSubGroupName_1; }
            set { _mAccountSubGroupName_1 = value; }
        }

        /// <summary>
        /// The AccountSubGroupName_2 field in the DB
        /// </summary>
        public string AccountSubGroupName_2
        {
            get { return _mAccountSubGroupName_2; }
            set { _mAccountSubGroupName_2 = value; }
        }

        /// <summary>
        /// The AccountGroup field in the DB
        /// </summary>
        public int AccountGroup
        {
            get { return _mAccountGroup; }
            set { _mAccountGroup = value; }
        }


        #endregion
    }
}