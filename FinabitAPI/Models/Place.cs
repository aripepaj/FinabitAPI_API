using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class Place : BaseClass
    {
        public string PlaceName { get; set; }

        public override string ToString()
        {
            return this.PlaceName;
        }

        public Place()
        {
            PlaceName = "";
        }

    }
}