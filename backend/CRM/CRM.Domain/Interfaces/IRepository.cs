using CRM.Domain.Base;

namespace CRM.Domain.Interfaces
{
    public interface IRepository<T> where T : AggregateRoot
    {
    }
}
