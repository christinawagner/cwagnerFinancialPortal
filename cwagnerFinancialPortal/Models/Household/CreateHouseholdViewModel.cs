using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Household
{
    public class CreateHouseholdViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}