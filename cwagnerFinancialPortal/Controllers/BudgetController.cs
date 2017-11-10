using cwagnerFinancialPortal.Domain;
using cwagnerFinancialPortal.Domain.Budgets;
using cwagnerFinancialPortal.Extensions;
using cwagnerFinancialPortal.Models.Budget;
using System.Linq;
using System.Web.Mvc;

namespace cwagnerFinancialPortal.Controllers
{
    public class BudgetController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly BudgetManager _budgetManager;

        public BudgetController()
        {
            _db = new ApplicationDbContext();
            _budgetManager = new BudgetManager(_db);
        }

        // GET: Budget
        public ActionResult Index()
        {
            var householdId = User.Identity.GetHouseholdId();
            var model = new BudgetIndexViewModel();

            if(householdId != null)
            {
                model.Budgets = _budgetManager.GetAll(householdId.Value).Select(b => new BudgetViewModel { Id = b.Id, Total = b.Total, Duration = b.Duration }).ToList();
            }
            return View(model);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBudgetViewModel model)
        {
            var householdId = User.Identity.GetHouseholdId();
            if(ModelState.IsValid && householdId != null)
            {
                _budgetManager.Add(new Budget { HouseholdId = householdId.Value, Total = model.Total, Duration = model.Duration });
            }
            return RedirectToAction(nameof(Index));
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBudgetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var budget = _budgetManager.Get(model.Id);
                budget.Total = model.Total;
                budget.Duration = model.Duration;
                _budgetManager.Edit(budget);
            }
            return RedirectToAction("Index");
        }

        //POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _budgetManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}