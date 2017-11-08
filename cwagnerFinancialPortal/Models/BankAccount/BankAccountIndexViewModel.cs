using cwagnerFinancialPortal.Models.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models
{
    public class BankAccountIndexViewModel
    {
        public List<BankAccountViewModel> Accounts { get; set; }
    }
}