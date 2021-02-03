using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Podium_API.Models;

namespace Podium_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>(settings.ProductCollectionName);

            if (!_products.AsQueryable().Any())
            {
                // Seed Data Here
                List<Product> initialData = new List<Product>()
                {
                    new Product("Bank A", 2, "Variable", 60),
                    new Product("Bank B", 3, "Fixed", 60),
                    new Product("Bank C", 4, "Variable", 90)
                };
                _products.InsertMany(initialData);
            }
        }

        public async Task<List<Product>> FindAvailableProductsAsync(decimal propertyValue, decimal depositAmount)
        {
            List<Product> availableProducts = new List<Product>();
            decimal LTV = ((propertyValue - depositAmount)/ propertyValue)*100;

            if(LTV < 90) {
                availableProducts = await _products.FindAsync(x => x.MinimumLTV > LTV).Result.ToListAsync();
            }

            return availableProducts;
        }
    }
}
