using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using cwagnerFinancialPortal.Domain.Households;
using System.Collections.Generic;
using cwagnerFinancialPortal.Domain.Budgets;
using cwagnerFinancialPortal.Domain.Transactions;
using cwagnerFinancialPortal.Domain.BankAccounts;

namespace cwagnerFinancialPortal.Domain.Users
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? HouseholdId { get; set; }
        public string ProfilePic { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual Household Household { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FullName", FullName));
            userIdentity.AddClaim(new Claim("ProfilePic", ProfilePic ?? string.Empty));
            userIdentity.AddClaim(new Claim("HouseholdId", HouseholdId?.ToString() ?? string.Empty));
            userIdentity.AddClaim(new Claim("HouseholdName", Household?.Name ?? string.Empty));
            return userIdentity;
        }
    }    
}