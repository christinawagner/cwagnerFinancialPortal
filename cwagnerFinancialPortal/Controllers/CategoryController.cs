using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cwagnerFinancialPortal.Domain.Categories;
using cwagnerFinancialPortal.Extensions;
using cwagnerFinancialPortal.Models.Category;
using cwagnerFinancialPortal.Domain;

namespace cwagnerFinancialPortal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoryManager _manager;

        public CategoryController()
        {
            _db = new ApplicationDbContext();
            _manager = new CategoryManager(_db);
        }

        // GET: Category
        public ActionResult Index()
        {
            var householdId = User.Identity.GetHouseholdId();
            var model = new CategoryIndexViewModel();

            if(householdId != null)
            {
                model.Categories = _manager.GetAll(householdId.Value).Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToList();
            }
            return View();
        }

        // POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel model)
        {
            var householdId = User.Identity.GetHouseholdId();
            if (ModelState.IsValid && householdId != null)
            {
                _manager.Add(new Category { HouseholdId = householdId.Value, Name = model.Name });
            }
            return RedirectToAction(nameof(Index));
        }

        //POST: EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var householdId = User.Identity.GetHouseholdId();
                var category = _manager.Get(model.Id, householdId.Value);
                category.Name = model.Name;
                _manager.Edit(category);
            }
            return RedirectToAction("Index");
        }

        //POST: DELETE
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _manager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}