using CRM.Domain.Aggregates.UserAggregate;
using CRM.Infrastructure.EntityConfigurations.Customers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Contexts
{
    public sealed class CRMContext : DbContext
    {
        public DbSet<Customer> Users => Set<Customer>();
        public CRMContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}
