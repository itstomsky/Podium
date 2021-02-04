using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Podium_API.Models;

namespace Podium_API.Services
{
    public interface IProductService
    {
        public Task<List<Product>> FindAvailableProductsAsync(decimal propertyValue, decimal depositAmount);
    }
}
