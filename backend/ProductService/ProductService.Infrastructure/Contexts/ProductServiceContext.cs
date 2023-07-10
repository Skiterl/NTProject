using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities.Products;
using ProductService.Infrastructure.EntityConfigurations.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Contexts
{
    public sealed class ProductServiceContext:DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public ProductServiceContext(DbContextOptions<ProductServiceContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}