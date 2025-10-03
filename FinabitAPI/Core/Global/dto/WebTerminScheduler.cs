using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.Global.dto
{
    public class WebTerminScheduler
    {
        public DateTime Data { get; set; }
        public string Ora { get; set; }
        public string Termini { get; set; }
    }
}