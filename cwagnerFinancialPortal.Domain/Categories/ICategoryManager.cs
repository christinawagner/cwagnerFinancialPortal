using System.Linq;

namespace cwagnerFinancialPortal.Domain.Categories
{
    public interface ICategoryManager
    {
        int Add(Category category);
        void Delete(int id);
        void Edit(Category category);
        Category Get(int categoryId, int householdId);
        IQueryable<Category> GetAll(int householdId);
    }
}