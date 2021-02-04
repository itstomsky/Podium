using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Podium_API.Models;

namespace Podium_API.Services
{
    public interface IDatabaseContext
    {
       public IMongoCollection<User> Users { get; }
       public IMongoCollection<Product> Products { get; }

       public User FindUser(ObjectId id);
       public User FindUser(string email);
       public Product FindProduct(ObjectId id);
       public Task<List<Product>> FindProductsAsync(decimal LTV);
       public Task InsertUserAsync(User user);
       public Task ReplaceUserAsync(ObjectId id, User user);

    }
}
