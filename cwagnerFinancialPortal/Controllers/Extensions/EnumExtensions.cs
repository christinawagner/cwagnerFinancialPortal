using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cwagnerFinancialPortal.Extensions
{
    public static class EnumExtensions
    {
        public static string GetFriendlyName(this Enum val)
        {
            var displayName = val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false).SingleOrDefault() as DisplayAttribute;
            return displayName?.Name ?? val.ToString();
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> ToSelectList(this Enum val)
        {
            return Enum.GetValues(val.GetType()).Cast<Enum>().Select(x => new System.Web.Mvc.SelectListItem() { Text = x.GetFriendlyName(), Value = x.ToString() });
        }
    }
}