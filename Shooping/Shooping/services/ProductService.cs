
using Shooping.models;
using System.Collections.Generic;
using System.Linq;

namespace Shooping.services
{ 
    public class ProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new product
        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        // Get all products
        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        // Get product by ID
        public Product GetProductById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        // Update an existing product
        public void UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        // Delete a product
        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }
    }

}
