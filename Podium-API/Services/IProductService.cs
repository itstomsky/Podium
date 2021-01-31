using System;
using System.Collections.Generic;
using Podium_API.Models;

namespace Podium_API.Services
{
    public interface IProductService
    {
        public List<Product> FindAvailableProducts(decimal propertyValue, decimal depositAmount);
    }
}
