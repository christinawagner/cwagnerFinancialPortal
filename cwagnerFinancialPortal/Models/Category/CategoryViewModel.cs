﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanEdit { get; set; }
    }
}