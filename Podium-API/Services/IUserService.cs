using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Podium_API.Models;

namespace Podium_API.Services
{
    public interface IUserService
    {
        public Task<string> RegisterAsync(User user);
        public bool CheckUserId(string id);
        public bool ValidAge(string id);
    }
}
