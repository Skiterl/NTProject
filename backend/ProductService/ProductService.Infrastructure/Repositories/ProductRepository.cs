using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities.Products;
using ProductService.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductServiceContext _context;

        public ProductRepository(ProductServiceContext productContext)
        {
            _context = productContext;
        }
        public async Task<Product> Create(Product product)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));

            await _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(Guid id)
        {
            var existingProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if (existingProduct is null) return null;

            _context.Set<Product>().Remove(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Set<Product>().ToListAsync<Product>();
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _context.Set<Product>().FindAsync(id);

            if (product is null) return null;
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            var entry = _context.Update(product);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }
    }
}
