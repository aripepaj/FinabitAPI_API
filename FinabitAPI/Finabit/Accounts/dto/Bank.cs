using FinabitAPI.Core.Global.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Account.dto
{
    public class Bank : BaseClass
    {
        public Bank()
        {
            BankName = "";
            BankAccount = "";
        }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
    }
}