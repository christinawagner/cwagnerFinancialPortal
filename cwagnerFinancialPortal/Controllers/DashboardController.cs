using cwagnerFinancialPortal.Domain;
using cwagnerFinancialPortal.Domain.Budgets;
using cwagnerFinancialPortal.Extensions;
using cwagnerFinancialPortal.Models.Budget;
using cwagnerFinancialPortal.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cwagnerFinancialPortal.Controllers
{
    [Authorize]
    [RequireHttps]
    public class DashboardController : Controller
    {

        private readonly BudgetManager _budgetManager;

        public DashboardController()
        {
            var db = new ApplicationDbContext();
            _budgetManager = new BudgetManager(db);
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            var householdId = User.Identity.GetHouseholdId().Value;
            var budgets = _budgetManager.GetAll(householdId).Select(x => new SelectListItem { Text = x.BudgetItems.Sum(s => s.Amount).ToString(), Value = x.Id.ToString() });

            return View(new DashboardIndexViewModel { BudgetSelectList = new SelectList(budgets, "Value", "Text") });
        }

        public JsonResult GetBarChart(int budgetId)
        {
            var colors = new[] { "red", "green", "blue", "lightblue", "yellow" };
            var i = 0;

            var graphs = _budgetManager.GetBarGraph(budgetId).Select(g => new
            {
                name = g.Name,
                labels = g.Labels,
                datasets = g.DataSets.Select(s => new { label = s.Label, data = s.Data, backgroundColor = colors[++i > colors.Length ? 0 : i] })
            });
            
            return Json(graphs, JsonRequestBehavior.AllowGet);
        }

    }
}