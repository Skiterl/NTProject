using CRM.Domain.Interfaces;

namespace CRM.Domain.Aggregates.UserAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetById(Guid id);
        Task<List<Customer>> GetAll();
        Task<Customer> Create(Customer user);
        Task<Customer> Update(Customer user);
        Task<Customer> Delete(Guid id);
    }
}
