using cwagnerFinancialPortal.Domain.Budgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Budget
{
    public class DeleteBudgetViewModel
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public Duration Duration { get; set; }
    }
}