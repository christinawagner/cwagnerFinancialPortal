using cwagnerFinancialPortal.Domain.BankAccounts;
using cwagnerFinancialPortal.Domain.Budgets;
using cwagnerFinancialPortal.Domain.Households;
using cwagnerFinancialPortal.Domain.Transactions;
using cwagnerFinancialPortal.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace cwagnerFinancialPortal.Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Household> Households { get; set; }
        public DbSet<HouseholdInvite> HouseholdInvites { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
