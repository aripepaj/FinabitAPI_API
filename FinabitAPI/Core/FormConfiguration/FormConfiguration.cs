using FinabitAPI.Core.Global.dto;
using FinabitAPI.Core.User;

namespace FinabitAPI.Core.FormConfiguration
{
    public class FormConfiguration : BaseClass
    {
        private bool _mAllowDelete;
        private bool _mAllowInsert;
        private bool _mAllowPrint;
        private bool _mAllowShow;
        private bool _mAllowUpdate;
        private bool _mEnabled;
        private Forms _mForm = new Forms();
        private bool _mReadOnly;
        private Roles _mRole = new Roles();

        public bool AllowDelete
        {
            get
            {
                return this._mAllowDelete;
            }
            set
            {
                this._mAllowDelete = value;
            }
        }

        public bool AllowInsert
        {
            get
            {
                return this._mAllowInsert;
            }
            set
            {
                this._mAllowInsert = value;
            }
        }

        public bool AllowPrint
        {
            get
            {
                return this._mAllowPrint;
            }
            set
            {
                this._mAllowPrint = value;
            }
        }

        public bool AllowShow
        {
            get
            {
                return this._mAllowShow;
            }
            set
            {
                this._mAllowShow = value;
            }
        }

        public bool AllowUpdate
        {
            get
            {
                return this._mAllowUpdate;
            }
            set
            {
                this._mAllowUpdate = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this._mEnabled;
            }
            set
            {
                this._mEnabled = value;
            }
        }

        public Forms Form
        {
            get
            {
                return this._mForm;
            }
            set
            {
                this._mForm = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this._mReadOnly;
            }
            set
            {
                this._mReadOnly = value;
            }
        }

        public Roles Role
        {
            get
            {
                return this._mRole;
            }
            set
            {
                this._mRole = value;
            }
        }

        public bool AllowChangeStatus { get; set; }
    }
}