using CRM.Domain.Aggregates.UserAggregate;
using CRM.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CRMContext _crmContext;

        public CustomerRepository(Contexts.CRMContext customerContext)
        {
            _crmContext = customerContext;
        }

        public async Task<Customer> Create(Customer customer)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));

            await _crmContext.Set<Customer>().AddAsync(customer);
            await _crmContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Delete(Guid id)
        {
            var existingUser = _crmContext.Users.FirstOrDefault(x => x.Id == id);
            if (existingUser is null) return null;

            _crmContext.Set<Customer>().Remove(existingUser);
            await _crmContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _crmContext.Set<Customer>().ToListAsync<Customer>();
        }

        public async Task<Customer> GetById(Guid id)
        {
            var customer = await _crmContext.Set<Customer>().FindAsync(id);

            if (customer is null) return null;
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            var entry = _crmContext.Update(customer);
            await _crmContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task SaveShanges()
        {
            await _crmContext.SaveChangesAsync();
        }
    }
}
