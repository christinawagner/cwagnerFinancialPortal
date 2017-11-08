using cwagnerFinancialPortal.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Transaction
{
    public class EditTransactionViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public Category Category { get; set; }
        public bool IsReconciled { get; set; }
    }
}