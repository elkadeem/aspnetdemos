namespace WebAPIDemo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebAPIDemo.Entities.EmployeesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebAPIDemo.Entities.EmployeesDbContext context)
        {
            context.Employees.AddOrUpdate(new Entities.Employee {
              Id = 1,
              FirstName = "Ahmed",
              LastName = "Ali"
            }, new Entities.Employee
            {
                Id = 2,
                FirstName = "Ali",
                LastName = "Ali"
            },
            new Entities.Employee
            {
                Id = 3,
                FirstName = "Mohamed",
                LastName = "Ali"
            });

            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
