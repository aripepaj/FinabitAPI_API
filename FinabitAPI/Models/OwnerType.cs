using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finabit_API.Models
{
    public class OwnerType
    {
        private string _mOwnerTypeName;

        public override string ToString()
        {
            return this.OwnerTypeName;
        }

        public string OwnerTypeName
        {
            get
            {
                return this._mOwnerTypeName;
            }
            set
            {
                this._mOwnerTypeName = value;
            }
        }

        public int ID { get; set; }
    }
}