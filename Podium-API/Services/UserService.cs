using System;
using Podium_API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

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

        public bool CheckUserId(string id)
        {
            try
            {
                User user = _users.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();

                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception)
            {
                // Id which cannot be parsed are also invalid
                return false;
            }
        }

        public bool ValidAge(string id)
        {
            try
            {
                User user = _users.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();

                if (user != null)
                {
                    int userAge = computeAge(user.DateOfBirth);

                    if(userAge >= 18)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                // Id which cannot be parsed are also invalid
                return false;
            }
        }

        public async Task<string> RegisterAsync(User user)
        {
            var oldUser = _users.Find(x => x.Email == user.Email).FirstOrDefault();

            if (oldUser == null)
            {
                await _users.InsertOneAsync(user);
            }
            else
            {
                user.Id = oldUser.Id;
                await _users.ReplaceOneAsync(x => x.Id == oldUser.Id, user);
            }

            return user.Id.ToString();
        }

        private int computeAge(DateTime dob)
        {
            int age = DateTime.Now.Year - dob.Year;
            int m = DateTime.Now.Month - dob.Month;

            if (m < 0 || (m == 0 && DateTime.Now.Date < dob.Date))
            {
                age--;
            }

            return age;
        }
    }
}
