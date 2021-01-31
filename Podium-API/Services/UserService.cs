using System;
using Podium_API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podium_API.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task<string> RegisterAsync(User user)
        {
            var oldUser = _users.Find(x => x.Email == user.Email).FirstOrDefault();

            if (oldUser != null)
            {
                await _users.InsertOneAsync(user);
            }
            else
            {
                await _users.ReplaceOneAsync(x => x.Id == oldUser.Id, user);
            }

            return user.Id;
        }
    }
}
