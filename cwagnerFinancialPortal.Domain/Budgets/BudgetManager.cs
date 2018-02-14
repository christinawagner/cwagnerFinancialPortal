using cwagnerFinancialPortal.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace cwagnerFinancialPortal.Domain.Budgets
{
    public class BudgetManager : IBudgetManager
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
            if (budget != null)
            {
                foreach (var item in budget.BudgetItems.ToList())
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

        //DASHBOARD
        public IEnumerable<Chart<Decimal>> GetBarGraph(int id)
        {
            var budget = db.Budgets.Find(id);

            IQueryable<Transaction> previousTransactions;
            IQueryable<Transaction> currentTransactions;

            var today = DateTimeOffset.Now.Date;

            switch (budget.Duration)
            {
                case Duration.Weekly:
                    var firstDayOfLastWeek = today.FirstDayOfWeek().AddDays(-7);
                    var lastDayOfLastWeek = today.FirstDayOfWeek().AddDays(-1);
                    var firstDayOfWeek = today.FirstDayOfWeek();
                    var lastDayOfWeek = today.LastDayOfWeek();

                    previousTransactions = db.Transactions.Where(t => t.BankAccount.HouseholdId == budget.HouseholdId && (t.Date >= firstDayOfLastWeek && t.Date <= lastDayOfLastWeek));
                    currentTransactions = db.Transactions.Where(t => t.BankAccount.HouseholdId == budget.HouseholdId && (t.Date >= firstDayOfWeek && t.Date <= lastDayOfWeek));
                    break;
                default:
                case Duration.Monthly:
                    var firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
                    var lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);

                    var firstDayOfMonth = lastDayOfLastMonth.AddDays(1);
                    var lastDayOfMonth = firstDayOfLastMonth.AddMonths(2).AddDays(-1);

                    previousTransactions = db.Transactions.Where(t => t.BankAccount.HouseholdId == budget.HouseholdId && (t.Date >= firstDayOfLastMonth && t.Date <= lastDayOfLastMonth));
                    currentTransactions = db.Transactions.Where(t => t.BankAccount.HouseholdId == budget.HouseholdId && (t.Date >= firstDayOfMonth && t.Date <= lastDayOfMonth));
                    break;

            }

            var budgetAmounts = db.BudgetItems.Where(b => b.BudgetId == budget.Id).GroupBy(g => g.Category.Name).Select(s => new { CategoryName = s.Key, BudgetAmount = s.DefaultIfEmpty().Sum(a => a.Amount) });
            var previousTransactionGroups = previousTransactions.GroupBy(g => g.Category.Name);
            var currentTransactionGroups = currentTransactions.GroupBy(g => g.Category.Name);


            var previousData = (from amount in budgetAmounts
                            join transactions in previousTransactionGroups on amount.CategoryName equals transactions.Key
                            into temp
                            from transactions in temp.DefaultIfEmpty()
                            select new BudgetDataItem
                            {
                                Category = amount.CategoryName,
                                Budgeted = amount.BudgetAmount,
                                Spent = -transactions.Select(s => s.Amount).DefaultIfEmpty().Sum()
                            })
                .OrderBy(o => o.Category)
                .ToList();

            var currentData = (from amount in budgetAmounts
                            join transactions in currentTransactionGroups on amount.CategoryName equals transactions.Key
                            into temp
                            from transactions in temp.DefaultIfEmpty()
                            select new BudgetDataItem
                            {
                                Category = amount.CategoryName,
                                Budgeted = amount.BudgetAmount,
                                Spent = -transactions.Select(s => s.Amount).DefaultIfEmpty().Sum()
                            })
                .OrderBy(o => o.Category)
                .ToList();

            return new Chart<Decimal>[]
            {
                new Chart<Decimal>
                {
                    Name = "Current",
                    Labels = currentData.Select(s => s.Category).ToArray(),
                    DataSets =  new ChartDataSet<Decimal>[]
                    {
                        new ChartDataSet<Decimal> { Label = "Budgeted", Data = currentData.Select(s => s.Budgeted) },
                        new ChartDataSet<Decimal> { Label = "Spent", Data = currentData.Select(s => s.Spent) }
                    }
                },

                new Chart<Decimal>
                {
                    Name = "Previous",
                    Labels = previousData.Select(s => s.Category).ToArray(),
                    DataSets = new ChartDataSet<decimal>[]
                    {
                        new ChartDataSet<decimal> { Label = "Budgeted", Data = previousData.Select(s => s.Budgeted) },
                        new ChartDataSet<decimal> { Label = "Spent", Data = previousData.Select(s => s.Spent) }
                    }
                }

            };

        }

        private class BudgetDataItem
        {
            public string Category { get; set; }
            public Decimal Budgeted { get; set; }
            public Decimal Spent { get; set; }
        }
    }

}
