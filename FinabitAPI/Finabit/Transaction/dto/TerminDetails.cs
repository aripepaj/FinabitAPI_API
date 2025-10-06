using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Transaction.dto
{
    public class TerminDetails
    {
        public int TerminID { get; set; }
        public int UserID { get; set; }
        public decimal Value { get; set; }
        public decimal PaidValue { get; set; }
    }
}