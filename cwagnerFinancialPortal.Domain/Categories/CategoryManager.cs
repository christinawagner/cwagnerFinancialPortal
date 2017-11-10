using System;
using System.Data.Entity;
using System.Linq;

namespace cwagnerFinancialPortal.Domain.Categories
{
    public class CategoryManager
    {
        private readonly ApplicationDbContext db;

        public CategoryManager(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public Category Get(int categoryId, int householdId)
        {
            var category = db.Categories.SingleOrDefault(c => c.HouseholdId == householdId && c.Id == categoryId);
            return category;
        }

        public IQueryable<Category> GetAll (int householdId)
        {
            return db.Categories.Where(c => c.HouseholdId == householdId || c.HouseholdId == null);
        }

        public int Add(Category category)
        {
            if(category.HouseholdId == null)
            {
                throw new ArgumentException(nameof(category.HouseholdId));
            }
            db.Categories.Add(category);
            db.SaveChanges();

            return (category.Id);
        }

        public void Edit(Category category)
        {
            if(category.HouseholdId != null)
            {
                if(!db.Categories.Local.Any(c => c.Id == category.Id))
                {
                    db.Categories.Attach(category);
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var category = db.Categories.Find(id);
            if(category != null && category.HouseholdId != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }
    }
}
