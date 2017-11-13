using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        //BUDGET
        public Budget Get(int id, int householdId)
        {
            var budget = db.Budgets.SingleOrDefault(b => b.Id == id && b.HouseholdId == householdId);
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

        public Budget Details(int id)
        {
            var budget = db.Budgets.Find(id);
            return budget;
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
                foreach(var item in budget.BudgetItems.ToList())
                {
                    db.BudgetItems.Remove(item);
                }
                db.Budgets.Remove(budget);
                db.SaveChanges();
            }
        }

        //BUDGET ITEMS
        public BudgetItem GetItem(int id)
        {
            var budgetItem = db.BudgetItems.SingleOrDefault(i => i.Id == id);
            return budgetItem;
        }

        public IQueryable<BudgetItem> GetAllItem(int budgetId)
        {
            var budgetItem = db.BudgetItems.Where(i => i.BudgetId == budgetId);
            return budgetItem;
        }

        public int AddItem(BudgetItem budgetItem)
        {
            db.BudgetItems.Add(budgetItem);
            db.SaveChanges();

            return (budgetItem.Id);
        }

        public void EditItem(BudgetItem budgetItem)
        {
            db.Entry(budgetItem).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var budgetItem = db.BudgetItems.Find(id);
            if (budgetItem != null)
            {
                db.BudgetItems.Remove(budgetItem);
                db.SaveChanges();
            }
        }
    }
}
