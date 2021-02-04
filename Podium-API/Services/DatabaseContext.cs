using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Podium_API.Models;

namespace Podium_API.Services
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSettings _settings;
        private IMongoCollection<Product> _products;
        private IMongoCollection<User> _users;
        private readonly ILogger<DatabaseContext> _logger;

        public DatabaseContext(ILogger<DatabaseContext> logger, IMongoDatabase database, IDatabaseSettings settings)
        {
            try
            {
                _logger = logger;
                _database = database;
                _settings = settings;

                if (!Products.AsQueryable().Any())
                {
                    // Seeding Product Data
                    List<Product> initialData = new List<Product>()
                {
                    new Product("Bank A", 2, "Variable", 60),
                    new Product("Bank B", 3, "Fixed", 60),
                    new Product("Bank C", 4, "Variable", 90)
                };
                    Products.InsertMany(initialData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public IMongoCollection<Product> Products
        {
            get
            {
                if (_products is null)
                    _products = _database.GetCollection<Product>(_settings.ProductCollectionName);

                return _products;
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                if (_users is null)
                    _users = _database.GetCollection<User>(_settings.UserCollectionName);
                return _users;
            }
        }

        public Product FindProduct(ObjectId id)
        {
            Product product = Products.Find(x => x.Id == id).FirstOrDefault();

            return product;
        }

        public User FindUser(ObjectId id)
        {
            User user = Users.Find(x => x.Id == id).FirstOrDefault();

            return user;
        }

        public User FindUser(string email)
        {
            User user = Users.Find(x => x.Email == email).FirstOrDefault();

            return user;
        }

        public async Task<List<Product>> FindProductsAsync(decimal LTV)
        {
            List<Product> availableProducts = null;

            try
            {
                availableProducts = await Products.FindAsync(x => x.MinimumLTV > LTV).Result.ToListAsync();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return availableProducts;
        }

        public async Task InsertUserAsync(User user)
        {
            try
            {
                await Users.InsertOneAsync(user);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task ReplaceUserAsync(ObjectId id, User user)
        {
            try
            {
                await Users.ReplaceOneAsync(x => x.Id == id, user);
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
