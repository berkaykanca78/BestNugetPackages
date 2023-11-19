using BaseProject.Models;
using BaseWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseWebApp.Utilities.EFCore
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
