using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwagnerFinancialPortal.Domain.Budgets
{
    public class BudgetManager
    {
        private readonly ApplicationDbContext db;

        public BudgetManager(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public Budget Get(int householdId)
        {
            var budget = db.Budgets.SingleOrDefault(b => b.HouseholdId == householdId);
            return budget;
        }

        public IQueryable<Budget> GetAll(int householdId)
        {
            return db.Budgets.Where(b => b.HouseholdId == householdId);
        }

        public int Add(Budget budget)
        {
            db.Budgets.Add(budget);
            db.SaveChanges();

            return (budget.Id);
        }

        public void Edit(Budget budget)
        {
            db.Entry(budget).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var budget = db.Budgets.Find(id);
            if(budget != null)
            {
                db.Budgets.Remove(budget);
                db.SaveChanges();
            }
        }
    }
}
