using cwagnerFinancialPortal.Domain.Budgets;
using cwagnerFinancialPortal.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cwagnerFinancialPortal.Domain.Households
{
    public class Household
    {
        public Household()
        {
            Users = new HashSet<ApplicationUser>();
            Budgets = new HashSet<Budget>();
        }

        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
    }
}
