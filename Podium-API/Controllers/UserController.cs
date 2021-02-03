﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Podium_API.Models;
using Podium_API.Services;

namespace Podium_API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IDatabaseSettings databaseSettings)
        {
            _logger = logger;
            _userService = new UserService(databaseSettings);
        }



        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _logger.LogInformation($"Register called with {user?.ToString()}");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(user);

            return Ok(result);
        }

    }
}

