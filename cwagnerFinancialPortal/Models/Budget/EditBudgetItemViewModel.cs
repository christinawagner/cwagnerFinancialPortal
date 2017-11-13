using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cwagnerFinancialPortal.Models.Budget
{
    public class EditBudgetItemViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }

        public SelectList CategorySelectList { get; set; }
    }
}