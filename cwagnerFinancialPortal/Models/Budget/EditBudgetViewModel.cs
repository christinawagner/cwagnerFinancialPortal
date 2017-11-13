using cwagnerFinancialPortal.Domain.Budgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Budget
{
    public class EditBudgetViewModel
    {
        public int Id { get; set; }
        public Duration Duration { get; set; }
    }
}