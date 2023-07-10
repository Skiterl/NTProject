using CRM.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CRM.Infrastructure.EntityConfigurations.Customers
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Id");

            builder.OwnsOne(c => c.FirstName).Property(fn => fn.Value).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            builder.OwnsOne(c => c.LastName).Property(ln => ln.Value).HasColumnName("LastName").HasMaxLength(100).IsRequired();

            builder.OwnsOne(c => c.Email).Property(e => e.Value).HasColumnName("Email").HasMaxLength(50).IsRequired();
            builder.Navigation(c => c.Email).IsRequired();

            builder.OwnsOne(c => c.PhoneNumber).Property(pn => pn.Value).HasColumnName("PhoneNumber").HasMaxLength(15).IsRequired();
            builder.Navigation(c => c.PhoneNumber).IsRequired();

            builder.OwnsOne(c => c.Age).Property(a => a.Value).HasColumnName("Age").HasColumnType("integer").IsRequired();
            builder.Navigation(c => c.Age).IsRequired();

            builder.Property(c => c.CreatedAt).HasColumnName("CreatedDateTime").IsRequired();
        }
    }
}