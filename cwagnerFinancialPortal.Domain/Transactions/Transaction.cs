using cwagnerFinancialPortal.Domain.BankAccounts;
using cwagnerFinancialPortal.Domain.Categories;
using System;

namespace cwagnerFinancialPortal.Domain.Transactions
{
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
        public int CategoryId { get; set; }
        public TransactionType Type { get; set; }
        public bool IsReconciled { get; set; }

        public int BankAccountId { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual Category Category { get; set; }
    }
}
