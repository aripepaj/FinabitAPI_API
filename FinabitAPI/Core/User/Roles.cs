using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Core.User
{
    public class Roles : BaseClass
    {
        private string? _mRoleName;

        public override string ToString()
        {
            return this.RoleName ?? "";
        }

        public string? RoleName
        {
            get
            {
                return this._mRoleName;
            }
            set
            {
                this._mRoleName = value;
            }
        }
    }
}