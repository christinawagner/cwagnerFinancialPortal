using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Budget
{
    public class DeleteBudgetItemViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}