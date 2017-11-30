using cwagnerFinancialPortal.Domain.Categories;
using cwagnerFinancialPortal.Domain.Households;
using cwagnerFinancialPortal.Domain.Transactions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace cwagnerFinancialPortal.Domain.Budgets
{
    public enum Duration
    {
        Weekly,
        Monthly,
    }

    public class Budget
    {
        public Budget()
        {
            Transactions = new HashSet<Transaction>();
            Categories = new HashSet<Category>();
            BudgetItems = new HashSet<BudgetItem>();
        }

        public int Id { get; set; }
        [NotMapped]
        public decimal Total => BudgetItems?.Sum(b => b.Amount) ?? 0;
        public int HouseholdId { get; set; }
        public Duration Duration { get; set; }
 
        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }
    }
}
