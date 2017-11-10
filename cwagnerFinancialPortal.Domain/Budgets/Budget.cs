using cwagnerFinancialPortal.Domain.Categories;
using cwagnerFinancialPortal.Domain.Households;
using cwagnerFinancialPortal.Domain.Transactions;
using System.Collections.Generic;

namespace cwagnerFinancialPortal.Domain.Budgets
{
    public enum Duration
    {
        Weekly,
        Monthly,
        Yearly
    }

    public class Budget
    {
        public Budget()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public decimal Total { get; set; }
        public int HouseholdId { get; set; }
        public Duration Duration { get; set; }
 
        public virtual Household Household { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
