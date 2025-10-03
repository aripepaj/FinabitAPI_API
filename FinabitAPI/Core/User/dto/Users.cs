using FinabitAPI.Core.Global.dto;
using FinabitAPI.Finabit.Employees.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.User.dto
{
    public class Users : BaseClass
    {
        private Employees _mEmployee = new Employees();
        //private Roles _mRole = new Roles();
        private DateTime _mExpireDate;
        private string _mPassword;
        private bool _mStatus;
        private string _mUserName;
        private int _mRoleID;
        private int _mLanguageID;
        public string PIN { get; set; }
        public string PDAPIN { get; set; }
        public bool AllowReplication { get; set; }
        public string PIN2 { get; set; }
        public bool IsAuthoriser { get; set; }
        public bool DisableDateInDocuments { get; set; }

        private int _mDepartmentID;


        public int DepartmentID
        {
            get
            {
                return _mDepartmentID;
            }
            set
            {
                _mDepartmentID = value;
            }
        }

        public Employees Employee
        {
            get
            {
                return _mEmployee;
            }
            set
            {
                _mEmployee = value;
            }
        }

        //public Roles Role
        //{
        //    get
        //    {
        //        return this._mRole;
        //    }
        //    set
        //    {
        //        this._mRole = value;
        //    }
        //}

        public DateTime ExpireDate
        {
            get
            {
                return _mExpireDate;
            }
            set
            {
                _mExpireDate = value;
            }
        }

        public string Password
        {
            get
            {
                return _mPassword;
            }
            set
            {
                _mPassword = value;
            }
        }

        public bool Status
        {
            get
            {
                return _mStatus;
            }
            set
            {
                _mStatus = value;
            }
        }

        public string UserName
        {
            get
            {
                return _mUserName;
            }
            set
            {
                _mUserName = value;
            }
        }

        public override string ToString()
        {
            return _mUserName;
        }

        public DataTable Departments { get; set; }

        public DataTable Accounts { get; set; }
        public string CashAccountsID { get; set; }

        public string DefaultPartnerName { get; set; }

        public int DefaultPartnerID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }

        public bool IsDeleteWithAuthorization { get; set; }
        public bool HasConnections { get; set; }
        //public override string ()
        //{
        //    return this.RoleName;
        //}

        public int RoleID
        {
            get
            {
                return _mRoleID;
            }
            set
            {
                _mRoleID = value;
            }
        }

        public int LanguageID
        {
            get
            {
                return _mLanguageID;
            }

            set
            {
                _mLanguageID = value;
            }
        }

        public int PartnerID { get; set; }
        public bool IsEditWithAuthorization { get; set; }
        public string authoriserEmail { get; set; }
        public string Color { get; set; }
    }
}