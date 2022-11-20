using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Solutions_Task.Models
{
    public class ModelContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
