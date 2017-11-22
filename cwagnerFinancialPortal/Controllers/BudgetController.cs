using cwagnerFinancialPortal.Domain;
using cwagnerFinancialPortal.Domain.Budgets;
using cwagnerFinancialPortal.Domain.Categories;
using cwagnerFinancialPortal.Extensions;
using cwagnerFinancialPortal.Models.Budget;
using cwagnerFinancialPortal.Models.Category;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace cwagnerFinancialPortal.Controllers
{
    [Authorize]
    [RequireHttps]
    public class BudgetController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly BudgetManager _budgetManager;
        private readonly CategoryManager _categoryManager;

        public BudgetController()
        {
            _db = new ApplicationDbContext();
            _budgetManager = new BudgetManager(_db);
            _categoryManager = new CategoryManager(_db);
        }

        //================
        //-----BUDGET-----
        //================

        // GET: Budget
        public ActionResult Index()
        {
            var householdId = User.Identity.GetHouseholdId();
            var model = new BudgetIndexViewModel();

            if (householdId != null)
            {
                model.Budgets = _budgetManager.GetAll(householdId.Value).Select(b => new BudgetViewModel
                {
                    Id = b.Id,
                    Duration = b.Duration,
                    Total = b.BudgetItems.Select(s =>s.Amount).DefaultIfEmpty().Sum(),
                    BudgetItems = b.BudgetItems.Select(bi => new BudgetItemViewModel
                    {
                        Id = bi.Id,
                        Amount = bi.Amount,
                        CategoryId = bi.CategoryId,
                        CategoryName = bi.Category.Name
                    }).ToList()
                }).ToList();
                model.Categories = _categoryManager.GetAll(householdId.Value).Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name, CanEdit = c.HouseholdId.HasValue }).ToList();
            }
            return View(model);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBudgetViewModel model)
        {
            var householdId = User.Identity.GetHouseholdId();
            if (ModelState.IsValid && householdId != null)
            {
                _budgetManager.Add(new Budget { HouseholdId = householdId.Value, Duration = model.Duration });
            }
            return RedirectToAction(nameof(Index));
        }

        public PartialViewResult CreateBudgetModal()
        {
            return PartialView();
        }

        //GET: Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new BudgetDetailsViewModel();
            var householdId = User.Identity.GetHouseholdId();
            var budget = _budgetManager.Get(id, householdId.Value);
            model.Id = budget.Id;
            model.Total = budget.Total;
            model.Duration = budget.Duration;
            model.BudgetItems = budget.BudgetItems.ToList().Select(i => new BudgetItemViewModel
            {
                Id = i.Id,
                Amount = i.Amount,
                CategoryId = i.CategoryId
            }).ToList();
            return View(model);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBudgetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var householdId = User.Identity.GetHouseholdId();
                var budget = _budgetManager.Get(model.Id, householdId.Value);
                budget.Duration = model.Duration;
                _budgetManager.Edit(budget);
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult EditBudgetModal(int id)
        {
            var householdId = User.Identity.GetHouseholdId();
            var budget = _budgetManager.Get(id, householdId.Value);
            var model = new EditBudgetViewModel()
            {
                Duration = budget.Duration,
            };
            return PartialView(model);
        }

        //POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _budgetManager.Delete(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult DeleteBudgetModal(int id)
        {
            var householdId = User.Identity.GetHouseholdId();
            var budget = _budgetManager.Get(id, householdId.Value);
            var model = new DeleteBudgetViewModel
            {
                Id = budget.Id,
                Duration = budget.Duration,
                Total = budget.BudgetItems.Select(s => s.Amount).DefaultIfEmpty().Sum()
            };
            return PartialView(model);
        }

        //======================
        //-----BUDGET-ITEMS-----
        //======================

        public PartialViewResult BudgetItemListPartial(IEnumerable<BudgetItemViewModel> model)
        {
            return PartialView(model);
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem(CreateBudgetItemViewModel model)
        {
            var householdId = User.Identity.GetHouseholdId();
            if (ModelState.IsValid && householdId != null)
            {
                _budgetManager.AddItem(new BudgetItem { BudgetId = model.BudgetId, Amount = model.Amount, CategoryId = model.CategoryId });
            }
            return RedirectToAction("Index");
        }

        [Route("Budget/CreateBudgetItemModal/{budgetId:int}")]
        public PartialViewResult CreateBudgetItemModal(int budgetId)
        {
            var categories = _categoryManager.GetAll(User.Identity.GetHouseholdId().Value);
            var model = new CreateBudgetItemViewModel
            {
                BudgetId = budgetId,
                CategorySelectList = new SelectList(categories, "Id", "Name")
            };

            return PartialView(model);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(EditBudgetItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var budgetItem = _budgetManager.GetItem(model.Id);
                budgetItem.Amount = model.Amount;
                budgetItem.CategoryId = model.CategoryId;
                _budgetManager.EditItem(budgetItem);
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult EditBudgetItemModal(int id)
        {
            var budgetItem = _budgetManager.GetItem(id);
            var categories = _categoryManager.GetAll(User.Identity.GetHouseholdId().Value);
            var model = new EditBudgetItemViewModel()
            {
                Amount = budgetItem.Amount,
                CategoryId = budgetItem.CategoryId,
                CategorySelectList = new SelectList(categories, "Id", "Name", budgetItem.CategoryId)
            };
            return PartialView(model);
        }

        //POST: Delete
        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            _budgetManager.DeleteItem(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult DeleteBudgetItemModal(int id)
        {
            var budgetItem = _budgetManager.GetItem(id);
            var model = new DeleteBudgetItemViewModel
            {
                Id = budgetItem.Id,
                Amount = budgetItem.Amount,
                CategoryId = budgetItem.CategoryId
            };
            return PartialView(model);
        }

        //====================
        //-----CATEGORIES-----
        //====================

        public PartialViewResult CategoryListPartial(IEnumerable<CategoryViewModel> model)
        {
            return PartialView(model);
        }

        // POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            var householdId = User.Identity.GetHouseholdId();
            if (ModelState.IsValid && householdId != null)
            {
                _categoryManager.Add(new Category { HouseholdId = householdId.Value, Name = model.Name });
            }
            return RedirectToAction(nameof(Index));
        }

        public PartialViewResult CreateCategoryModal()
        {
            return PartialView();
        }

        //POST: EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var householdId = User.Identity.GetHouseholdId();
                var category = _categoryManager.Get(model.Id, householdId.Value);
                category.Name = model.Name;
                _categoryManager.Edit(category);
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult EditCategoryModal(int id)
        {
            var householdId = User.Identity.GetHouseholdId();
            var category = _categoryManager.Get(id, householdId.Value);
            var model = new EditCategoryViewModel()
            {
                Name = category.Name
            };
            return PartialView(model);
        }

        //POST: DELETE
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            _categoryManager.Delete(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult DeleteCategoryModal(int id)
        {
            var householdId = User.Identity.GetHouseholdId();
            var category = _categoryManager.Get(id, householdId.Value);
            var model = new DeleteCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
            return PartialView(model);
        }
    }
}