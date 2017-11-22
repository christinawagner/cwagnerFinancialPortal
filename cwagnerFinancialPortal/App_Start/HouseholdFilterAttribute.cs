using cwagnerFinancialPortal.Extensions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cwagnerFinancialPortal
{
    internal class HouseholdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Household" 
                    && HttpContext.Current.User.Identity.GetHouseholdId() == null
                    && filterContext.ActionDescriptor.ActionName != "OverdraftNotification")
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Household" },
                            { "action", "Index" }
                        });
                }
            }
        }
    }
}