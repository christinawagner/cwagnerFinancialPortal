namespace cwagnerFinancialPortal.Domain.Migrations
{
    using cwagnerFinancialPortal.Domain.Categories;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category { Name = DefaultCategories.Adjustment },
                new Category { Name = DefaultCategories.Entertainment },
                new Category { Name = DefaultCategories.Food },
                new Category { Name = DefaultCategories.Gas },
                new Category { Name = DefaultCategories.Income },
                new Category { Name = DefaultCategories.Other }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
