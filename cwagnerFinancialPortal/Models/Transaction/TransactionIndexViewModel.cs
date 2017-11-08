using cwagnerFinancialPortal.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models
{
    public class TransactionIndexViewModel
    {
        public List<TransactionViewModel> Transactions { get; set; }
    }
}