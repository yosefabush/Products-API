using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Domain.Entities;

namespace Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Product>> GetAllAsync() => await _dbContext.Products.AsNoTracking().ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _dbContext.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetByNameAsync(string name) => 
            await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(name))
                .ToListAsync();

        public async Task<Product> AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
