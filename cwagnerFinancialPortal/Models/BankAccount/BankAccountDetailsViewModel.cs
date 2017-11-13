using cwagnerFinancialPortal.Models.Transaction;
using System.Collections.Generic;

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