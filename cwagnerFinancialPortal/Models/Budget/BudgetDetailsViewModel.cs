using cwagnerFinancialPortal.Domain.Budgets;
using System.Collections.Generic;

namespace cwagnerFinancialPortal.Models.Budget
{
    public class BudgetDetailsViewModel
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public Duration Duration { get; set; }

        public List<BudgetItemViewModel> BudgetItems { get; set; }
    }
}