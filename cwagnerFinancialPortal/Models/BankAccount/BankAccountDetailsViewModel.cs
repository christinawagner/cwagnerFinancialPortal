using cwagnerFinancialPortal.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.BankAccount
{
    public class BankAccountDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public List<TransactionViewModel> Transactions { get; set; }
    }
}