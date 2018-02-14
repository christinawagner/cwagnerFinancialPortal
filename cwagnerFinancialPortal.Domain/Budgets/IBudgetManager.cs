using System.Collections.Generic;
using System.Linq;

namespace cwagnerFinancialPortal.Domain.Budgets
{
    public interface IBudgetManager
    {
        int Add(Budget budget);
        int AddItem(BudgetItem budgetItem);
        void Delete(int id);
        void DeleteItem(int id);
        Budget Details(int id);
        void Edit(Budget budget);
        void EditItem(BudgetItem budgetItem);
        Budget Get(int id, int householdId);
        IQueryable<Budget> GetAll(int householdId);
        IQueryable<BudgetItem> GetAllItem(int budgetId);
        IEnumerable<Chart<decimal>> GetBarGraph(int id);
        BudgetItem GetItem(int id);
    }
}