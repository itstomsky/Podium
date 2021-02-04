using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Podium_API.Models;
using Podium_API.Services;

namespace Podium_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IUserService _userService;


        public ProductController(ILogger<ProductController> logger, IUserService userService, IProductService productService)
        {
            _logger = logger;
            _userService = userService;
            _productService = productService;
        }

        

        [HttpGet]
        [Route("{id}/{propertyValue}/{depositAmount}")]
        public async Task<IActionResult> FindProducts([FromRoute]string id, [FromRoute] decimal propertyValue, [FromRoute] decimal depositAmount)
        {
            _logger.LogInformation($"FindProducts called with {id}, {propertyValue} and {depositAmount}");

            List<Product> availableProducts = new List<Product>();

            if (_userService.CheckUserId(id))
            {
                if (_userService.ValidAge(id))
                {
                    availableProducts = await _productService.FindAvailableProductsAsync(propertyValue, depositAmount);

                    if(availableProducts == null)
                    {
                        availableProducts = new List<Product>();
                    }
                }
            }
            else {
                return Unauthorized();
            }

            return Ok(availableProducts);
        }
    }
}
