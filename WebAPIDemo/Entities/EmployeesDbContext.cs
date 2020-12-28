using System.Data.Entity;

namespace WebAPIDemo.Entities
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext() : base("EmployeesConnectionString")
        {
            Database.SetInitializer<EmployeesDbContext>(null);
        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}