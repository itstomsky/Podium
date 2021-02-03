using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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


        public ProductController(ILogger<ProductController> logger, IDatabaseSettings databaseSettings)
        {
            _logger = logger;
            _userService = new UserService(databaseSettings);
            _productService = new ProductService(databaseSettings);
        }

        

        [HttpGet]
        [Route("{id}/{propertyValue}/{depositAmount}")]
        public async Task<IActionResult> FindProducts([FromRoute]string id, [FromRoute] decimal propertyValue, [FromRoute] decimal depositAmount)
        {
            _logger.LogInformation($"FindProducts called with {id}, {propertyValue} and {depositAmount}");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_userService.CheckUserId(id))
            {
                if (_userService.ValidAge(id))
                {
                    return Ok(await _productService.FindAvailableProductsAsync(propertyValue, depositAmount));
                }
                else
                {
                    // return empty list
                    return Ok(new List<Product>()); 
                }
            }
            else {
                return Unauthorized();
            } 
        }
    }
}
