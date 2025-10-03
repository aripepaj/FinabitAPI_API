using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.Global.dto
{
    public class OwnerType
    {
        private string _mOwnerTypeName;

        public override string ToString()
        {
            return OwnerTypeName;
        }

        public string OwnerTypeName
        {
            get
            {
                return _mOwnerTypeName;
            }
            set
            {
                _mOwnerTypeName = value;
            }
        }

        public int ID { get; set; }
    }
}