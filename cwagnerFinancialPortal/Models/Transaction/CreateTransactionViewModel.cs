using cwagnerFinancialPortal.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cwagnerFinancialPortal.Models.Transaction
{
    public class CreateTransactionViewModel
    {
        public int BankAccountId { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public int CategoryId { get; set; }
        public bool IsReconciled { get; set; }

        public SelectList CategorySelectList { get; set; }
    }
}