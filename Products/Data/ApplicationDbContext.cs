using Products.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Products.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
