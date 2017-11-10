using cwagnerFinancialPortal.Domain.Households;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwagnerFinancialPortal.Domain.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HouseholdId { get; set; }

        public Household Household { get; set; }
    }
}
