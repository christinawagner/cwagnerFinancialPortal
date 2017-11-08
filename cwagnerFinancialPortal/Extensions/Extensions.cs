using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace cwagnerFinancialPortal.Extensions
{
    public static class Extensions
    {
        public static string GetFullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            return claim?.Value ?? string.Empty;
        }

        public static string ProfilePicture(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ProfilePic");
            return claim?.Value ?? string.Empty;
        }

        public static int? GetHouseholdId(this IIdentity user)
        {
            var claimsIdentity = (ClaimsIdentity)user;
            var householdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "HouseholdId");

            if (!string.IsNullOrWhiteSpace(householdClaim?.Value))
            {
                return Int32.Parse(householdClaim.Value);
            }
            else
                return null;
        }

        public static string GetHouseholdName(this IIdentity user)
        {
            var claimsIdentity = (ClaimsIdentity)user;
            var householdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "HouseholdName");

            if (!string.IsNullOrWhiteSpace(householdClaim?.Value))
            {
                return householdClaim.Value;
            }
            else
                return null;
        }

        public static async Task RefreshAuthentication(this HttpContext context)
        {
            var owin = context.GetOwinContext();

            if(context.User.Identity.IsAuthenticated)
            {
                var signInManager = owin.Get<ApplicationSignInManager>();
                var userManager = owin.GetUserManager<ApplicationUserManager>();

                var userId = context.User.Identity.GetUserId();

                var user = userManager.FindById(userId);

                owin.Authentication.SignOut();

                var isPersistent = false;

                var authContext = await owin.Authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie);
                if (authContext != null)
                {
                    var aProperties = authContext.Properties;
                    isPersistent = aProperties.IsPersistent;
                }

                await signInManager.SignInAsync(user, isPersistent, false);
            }
        }
    }
}