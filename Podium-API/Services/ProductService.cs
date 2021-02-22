using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Podium_API.Models;
using Microsoft.Extensions.Logging;


namespace Podium_API.Services
{
    public class ProductService : IProductService
    {
        private IDatabaseContext _databaseContext;
        private readonly ILogger<ProductService> _logger;


        public ProductService(ILogger<ProductService> logger, IDatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        public async Task<List<Product>> FindAvailableProductsAsync(decimal propertyValue, decimal depositAmount)
        {
            List<Product> availableProducts = new List<Product>();
            decimal LTV = ((propertyValue - depositAmount)/ propertyValue)*100;

            try
            {
                if (LTV < 90)
                {
                    availableProducts = await _databaseContext.FindProductsAsync(LTV);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (availableProducts == null)
            {
                availableProducts = new List<Product>();
            }

            return availableProducts;
        }
    }
}
