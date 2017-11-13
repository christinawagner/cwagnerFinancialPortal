using cwagnerFinancialPortal.Domain.Categories;

namespace cwagnerFinancialPortal.Domain.Budgets
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public int BudgetId { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Category Category { get; set; }
    }
}
