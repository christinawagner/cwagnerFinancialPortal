using cwagnerFinancialPortal.Models.Category;
using System.Collections.Generic;

namespace cwagnerFinancialPortal.Models.Budget
{
    public class BudgetIndexViewModel
    {
        public List<BudgetViewModel> Budgets { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}