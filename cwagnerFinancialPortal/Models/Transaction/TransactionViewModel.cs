using cwagnerFinancialPortal.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Transaction
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [Display(Name="Category")]
        public string CategoryName { get; set; }
        public TransactionType Type { get; set; }
        [Display(Name="Reconciled?")]
        public bool IsReconciled { get; set; }

        private string ToYesNo(bool b)
        {
            return b ? "Yes" : "No";
        }

        public string IsReconciledString
        {
            get { return this.ToYesNo(this.IsReconciled); }
        }
    }
}