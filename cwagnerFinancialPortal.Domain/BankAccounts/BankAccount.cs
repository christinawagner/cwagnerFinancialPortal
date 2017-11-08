using cwagnerFinancialPortal.Domain.Households;
using cwagnerFinancialPortal.Domain.Transactions;
using cwagnerFinancialPortal.Domain.Users;
using System.Collections.Generic;

namespace cwagnerFinancialPortal.Domain.BankAccounts
{
    public class BankAccount
    {
        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public int HouseholdId { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
