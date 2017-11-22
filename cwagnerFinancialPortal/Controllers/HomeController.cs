using cwagnerFinancialPortal.Domain;
using cwagnerFinancialPortal.Domain.BankAccounts;
using cwagnerFinancialPortal.Extensions;
using cwagnerFinancialPortal.Models.Home;
using System.Linq;
using System.Web.Mvc;

namespace cwagnerFinancialPortal.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly BankAccountManager _bankAccountManager;

        public HomeController()
        {
            _db = new ApplicationDbContext();
            _bankAccountManager = new BankAccountManager(_db);
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult OverdraftNotification()
        {
            var householdId = User.Identity.GetHouseholdId();
            var model = new OverdraftNotificationViewModel();
            if(householdId != null)
            {
                model.OverdrawnAccounts = _bankAccountManager.GetAll(householdId.Value).Where(a => a.Balance < 0).Select(s => new BankAccountOverdraftViewModel
                {
                    Balance = s.Balance,
                    Name = s.Name
                }).ToList();
            }
            return PartialView("_OverdraftNotification", model);
        }
    }
}