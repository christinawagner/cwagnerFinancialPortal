using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models
{
    public class HouseholdIndexViewModel
    {
        public string Name { get; set; }
        public List<string> Members { get; set; }
    }
}