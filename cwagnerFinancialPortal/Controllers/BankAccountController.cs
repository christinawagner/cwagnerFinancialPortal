using System.Web.Mvc;
using cwagnerFinancialPortal.Domain.BankAccounts;
using cwagnerFinancialPortal.Extensions;
using cwagnerFinancialPortal.Models;
using cwagnerFinancialPortal.Models.BankAccount;
using System.Linq;
using cwagnerFinancialPortal.Domain.Transactions;
using cwagnerFinancialPortal.Models.Transaction;
using System.Collections.Generic;
using cwagnerFinancialPortal.Domain.Categories;
using cwagnerFinancialPortal.Domain;

namespace cwagnerFinancialPortal.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoryManager _categoryManager;
        private readonly BankAccountManager _manager;

        public BankAccountController()
        {
            _db = new ApplicationDbContext();
            _categoryManager = new CategoryManager(_db);
            _manager = new BankAccountManager(_db);
        }

        // GET: BankAccount
        public ActionResult Index()
        {
            var householdId = User.Identity.GetHouseholdId();
            var model = new BankAccountIndexViewModel();

            if (householdId != null)
            {
                model.Accounts = _manager.GetAll(householdId.Value).Select(a => new BankAccountViewModel { Id = a.Id, Name = a.Name, Balance = a.Balance }).ToList();
            }
            return View(model);
        }

        //GET: BankAccount DETAILS
        public ActionResult Details(int id)
        {
            var model = new BankAccountDetailsViewModel();
            var account = _manager.Get(id);
            model.Id = account.Id;
            model.Name = account.Name;
            model.Balance = account.Balance;
            model.Transactions = account.Transactions.ToList().Select(t => new TransactionViewModel
            {
                Id = t.Id,
                Date = t.Date,
                Amount = t.Amount,
                Description = t.Description,
                CategoryName = t.Category.Name,
                CategoryId = t.CategoryId,
                Type = t.Type,
                IsReconciled = t.IsReconciled
            }).ToList();
            return View(model);
        }

        public PartialViewResult CreateBankAccountModal()
        {
            return PartialView();
        }

        //POST: CREATE BankAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBankAccountViewModel model)
        {
            var householdId = User.Identity.GetHouseholdId();
            if (ModelState.IsValid && householdId != null)
            {
                _manager.Add(new BankAccount { HouseholdId = householdId.Value, Name = model.Name, Balance = model.Balance });
            }
            return RedirectToAction(nameof(Index));
        }

        public PartialViewResult EditBankAccountModal(int id)
        {
            var account = _manager.Get(id);
            var model = new EditBankAccountViewModel()
            {
                Name = account.Name
            };
            return PartialView(model);
        }

        //POST: EDIT BankAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBankAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _manager.Get(model.Id);
                account.Name = model.Name;
                _manager.Edit(account);
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult DeleteBankAccountModal(int id)
        {
            var account = _manager.Get(id);
            var model = new DeleteBankAccountViewModel
            {
                Id = account.Id,
                Name = account.Name
            };
            return PartialView(model);
        }

        //POST: DELETE BankAccount
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _manager.Delete(id);
            return RedirectToAction("Index");
        }

        //======================
        //-----TRANSACTIONS-----
        //======================

        public PartialViewResult CreateTransactionModal(int id)
        {
            var householdId = User.Identity.GetHouseholdId().Value;
            var availableCategories = _categoryManager.GetAll(householdId).ToList();
            var model = new CreateTransactionViewModel
            {
                BankAccountId = id,
                CategorySelectList = new SelectList(availableCategories, "Id", "Name")
            };
            return PartialView(model);
        }

        //POST: CREATE Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTransaction(CreateTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                _manager.AddTransaction(new Transaction
                {
                    BankAccountId = model.BankAccountId,
                    Date = model.Date,
                    Amount = model.Amount,
                    Description = model.Description,
                    Type = model.Type,
                    CategoryId = model.CategoryId,
                    IsReconciled = model.IsReconciled
                });
            }
            return RedirectToAction("Details", new { id = model.BankAccountId });
        }

        public PartialViewResult TransactionListPartial(IEnumerable<TransactionViewModel> model)
        {
            return PartialView(model);
        }

        public PartialViewResult EditTransactionModal(int id)
        {
            var householdId = User.Identity.GetHouseholdId().Value;
            var availableCategories = _categoryManager.GetAll(householdId).ToList();
            
            var transaction = _manager.GetTransaction(id);
            var model = new EditTransactionViewModel
            {
                Id = transaction.Id,
                Date = transaction.Date,
                Type = transaction.Type,
                Amount = transaction.Amount,
                Description = transaction.Description,
                CategoryId = transaction.CategoryId,
                IsReconciled = transaction.IsReconciled,
                CategorySelectList = new SelectList(availableCategories, "Id", "Name", transaction.CategoryId)
            };
            return PartialView(model);
        }

        //POST: EDIT Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTransaction(EditTransactionViewModel model)
        {
            var transaction = _manager.GetTransaction(model.Id);
            if (ModelState.IsValid)
            {
                transaction.Date = model.Date;
                transaction.Type = model.Type;
                transaction.Amount = model.Amount;
                transaction.Description = model.Description;
                transaction.CategoryId = model.CategoryId;
                transaction.IsReconciled = model.IsReconciled;
                _manager.EditTransaction(transaction);
            }
            return RedirectToAction("Details", new { id = transaction.BankAccountId });
        }

        public PartialViewResult DeleteTransactionModal(int id)
        {
            var transaction = _manager.GetTransaction(id);
            var model = new DeleteTransactionViewModel
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount
            };
            return PartialView(model);
        }

        //POST: DELETE Transaction
        [HttpPost]
        public ActionResult DeleteTransaction(int id)
        {
            var transaction = _manager.GetTransaction(id);
            _manager.DeleteTransaction(id);
            return RedirectToAction("Details", new { id = transaction.BankAccountId });
        }
    }
}