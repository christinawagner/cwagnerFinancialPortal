using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Home
{
    public class OverdraftNotificationViewModel
    {
        public List<BankAccountOverdraftViewModel> OverdrawnAccounts { get; set; }

        public OverdraftNotificationViewModel()
        {
            OverdrawnAccounts = new List<BankAccountOverdraftViewModel>();
        }

    }
}