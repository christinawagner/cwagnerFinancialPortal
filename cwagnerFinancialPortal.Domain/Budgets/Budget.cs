using cwagnerFinancialPortal.Domain.Households;
using cwagnerFinancialPortal.Domain.Transactions;
using System.Collections.Generic;

namespace cwagnerFinancialPortal.Domain.Budgets
{
    public class Budget
    {
        public Budget()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public decimal Total { get; set; }
        public int Duration { get; set; }

        public int HouseholdId { get; set; }

        public virtual Household Household { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
