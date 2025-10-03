using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.Global.dto
{
    public class PrintersForPOS : BaseClass
    {
        public PrintersForPOS()
        {
            PrinterAlias = "";
            PrinterPath = "";
        }
        public string PrinterAlias { get; set; }
        public string PrinterPath { get; set; }
    }
}