using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.EntityConfigurations.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");

            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnName("Price");
            builder.Property(p => p.CategoryName).HasColumnName("CategoryName").HasMaxLength(100);
            builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
        }
    }
}
