using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Lang> Langs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductsOfOrder> ProductsOfOrders { get; set; }
        public DbSet<ProductInfoLang> ProductInfos { get; set; }
    }
}
