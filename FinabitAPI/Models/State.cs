using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class State : BaseClass
    {
        public string StateName { get; set; }

        public override string ToString()
        {
            return this.StateName;
        }
        public State()
        {
            StateName = "";
        }
    }
}