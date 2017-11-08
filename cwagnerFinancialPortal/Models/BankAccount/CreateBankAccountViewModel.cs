using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.BankAccount
{
    public class CreateBankAccountViewModel
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}