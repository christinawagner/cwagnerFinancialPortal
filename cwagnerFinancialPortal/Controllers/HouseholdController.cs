using cwagnerFinancialPortal.Domain;
using cwagnerFinancialPortal.Domain.Households;
using cwagnerFinancialPortal.Extensions;
using cwagnerFinancialPortal.Models;
using cwagnerFinancialPortal.Models.Household;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace cwagnerFinancialPortal.Controllers
{
    [Authorize]
    public class HouseholdController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HouseholdManager _manager;

        public HouseholdController()
        {
            _db = new ApplicationDbContext();
            _manager = new HouseholdManager(_db);
        }

        // GET: Household
        public ActionResult Index()
        {
            var householdId = User.Identity.GetHouseholdId();
            var model = new HouseholdIndexViewModel();

            if (householdId != null)
            {
                var household = _manager.Get(householdId.Value);
                model.Name = household.Name;
                model.Members = household.Users.Select(s => s.FullName).ToList();
            }
            return View(model);
        }

        //POST: CREATE Household
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateHouseholdViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = _manager.Add(new Household { Name = model.Name });
                _manager.AddUserToHousehold(User.Identity.GetUserId(), id);
                await AddHouseholdClaim(id, model.Name);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Leave()
        {
            _manager.RemoveUserFromHousehold(User.Identity.GetUserId());
            await RemoveHouseholdClaim();
            return RedirectToAction(nameof(Index));
        }

        private async Task AddHouseholdClaim(int id, string name)
        {
            var identity = (ClaimsIdentity)User.Identity;

            // Call AddClaim, AddClaims or RemoveClaim on the user identity.
            var householdIdClaim = identity.Claims.SingleOrDefault(s => s.Type == "HouseholdId");
            if (householdIdClaim != null)
            {
                identity.RemoveClaim(householdIdClaim);
            }

            identity.AddClaim(new Claim("HouseholdId", id.ToString()));

            var householdNameClaim = identity.Claims.SingleOrDefault(c => c.Type == "HouseholdName");
            if (householdNameClaim != null)
            {
                identity.RemoveClaim(householdNameClaim);
            }

            identity.AddClaim(new Claim("HouseholdName", name));

            var context = Request.GetOwinContext();

            var authenticationContext =
                await context.Authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie);

            if (authenticationContext != null)
            {
                context.Authentication.AuthenticationResponseGrant = new AuthenticationResponseGrant(
                    identity,
                    authenticationContext.Properties);
            }
        }

        //leave household
        private async Task RemoveHouseholdClaim()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var householdIdClaim = identity.Claims.SingleOrDefault(s => s.Type == "HouseholdId");
            var householdNameClaim = identity.Claims.SingleOrDefault(c => c.Type == "HouseholdName");
            if (householdIdClaim != null)
            {
                // Call AddClaim, AddClaims or RemoveClaim on the user identity.
                identity.RemoveClaim(householdIdClaim);
            }
            if (householdNameClaim != null)
            {
                // Call AddClaim, AddClaims or RemoveClaim on the user identity.
                identity.RemoveClaim(householdNameClaim);
            }
            var context = Request.GetOwinContext();

            var authenticationContext =
                await context.Authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie);

            if (authenticationContext != null)
            {
                context.Authentication.AuthenticationResponseGrant = new AuthenticationResponseGrant(
                    identity,
                    authenticationContext.Properties);
            }
        }
    }
}