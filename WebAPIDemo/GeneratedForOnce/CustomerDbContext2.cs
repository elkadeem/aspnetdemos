using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebAPIDemo.GeneratedForOnce
{
    public partial class CustomerDbContext2 : DbContext
    {
        public CustomerDbContext2()
            : base("name=CustomerDbContext2")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
