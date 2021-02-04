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
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }



        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _logger.LogInformation($"Register called with {user?.ToString()}");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            string result = await _userService.RegisterAsync(user);

            return Ok(result);
        }

    }
}

