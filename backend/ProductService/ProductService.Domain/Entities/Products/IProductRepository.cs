using CRM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Entities.Products
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Delete(Guid id);
    }
}
