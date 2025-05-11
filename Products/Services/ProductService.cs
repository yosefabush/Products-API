using Products.Domain.Entities;
using Products.Repositories;
using Products.Models;

namespace Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<Product>> GetByNameAsync(string name) => await _repository.GetByNameAsync(name);

        public async Task<Product> CreateAsync(Product product) => await _repository.AddAsync(product);

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) return false;
            existing.Name = product.Name;
            existing.Price = product.Price;
            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) return false;
            await _repository.DeleteAsync(existing);
            return true;
        }

        public async Task<ProductInventoryDto?> GetProductInventoryAsync(int productId)
        {
            var stock = await _repository.GetStockByProductIdAsync(productId);
            if (stock is null)
                return null;
            return new ProductInventoryDto { ProductId = productId, Stock = stock.Value };
        }
    }
}
