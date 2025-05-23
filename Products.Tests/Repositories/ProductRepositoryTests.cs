using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Domain.Entities;
using Products.Repositories;
using Xunit;

namespace Products.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        // Generated by Copilot
        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Seed data
            using (var context = new ApplicationDbContext(options))
            {
                context.Products.AddRange(GetTestProducts());
                await context.SaveChangesAsync();
            }

            // Act & Assert
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ProductRepository(context);
                var products = await repository.GetAllAsync();
                
                Assert.Equal(3, products.Count());
                Assert.Equal("Test Product 1", products.First().Name);
            }
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var testProductId = 1;

            // Seed data
            using (var context = new ApplicationDbContext(options))
            {
                context.Products.AddRange(GetTestProducts());
                await context.SaveChangesAsync();
            }

            // Act & Assert
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ProductRepository(context);
                var product = await repository.GetByIdAsync(testProductId);
                
                Assert.NotNull(product);
                Assert.Equal(testProductId, product.Id);
                Assert.Equal("Test Product 1", product.Name);
            }
        }

        [Fact]
        public async Task AddAsync_AddsProductToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var newProduct = new Product 
            { 
                Name = "New Test Product", 
                Price = 15.99m 
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ProductRepository(context);
                var result = await repository.AddAsync(newProduct);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.Products.Count());
                var storedProduct = await context.Products.FirstOrDefaultAsync();
                Assert.NotNull(storedProduct);
                Assert.Equal("New Test Product", storedProduct.Name);
                Assert.Equal(15.99m, storedProduct.Price);
            }
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnMatchingProducts_WhenNameExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.AddRange(GetTestProducts());
                await context.SaveChangesAsync();
            }

            // Act & Assert
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ProductRepository(context);
                var products = await repository.GetByNameAsync("Test Product 1");
                Assert.Single(products);
                Assert.Equal("Test Product 1", products.First().Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.AddRange(GetTestProducts());
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ProductRepository(context);
                var product = await repository.GetByIdAsync(1);
                product!.Name = "Updated Product";
                await repository.UpdateAsync(product);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var updatedProduct = await context.Products.FindAsync(1);
                Assert.NotNull(updatedProduct);
                Assert.Equal("Updated Product", updatedProduct!.Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct_WhenProductExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.AddRange(GetTestProducts());
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ProductRepository(context);
                var product = await repository.GetByIdAsync(1);
                await repository.DeleteAsync(product!);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var deletedProduct = await context.Products.FindAsync(1);
                Assert.Null(deletedProduct);
                Assert.Equal(2, context.Products.Count());
            }
        }

        private List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1", Price = 10.99m },
                new Product { Id = 2, Name = "Test Product 2", Price = 20.99m },
                new Product { Id = 3, Name = "Test Product 3", Price = 30.99m }
            };
        }
    }
}