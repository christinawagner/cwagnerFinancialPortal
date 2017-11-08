using cwagnerFinancialPortal.Domain.BankAccounts;
using cwagnerFinancialPortal.Domain.Households;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace cwagnerFinancialPortal.Domain.Transactions
{
    public enum Category
    {
        Adjustment,
        Entertainment,
        Exercise,
        Food,
        Gas,
        Income,
        Medical,
        Work,
        Other
    }

    public enum TransactionType
    {
        Credit,
        Debit
    }

    public class Transaction
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public TransactionType Type { get; set; }
        public bool IsReconciled { get; set; }

        public int BankAccountId { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}
