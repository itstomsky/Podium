using System;
using Podium_API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;

namespace Podium_API.Services
{
    public class UserService : IUserService
    {
        private IDatabaseContext _databaseContext;
        private readonly ILogger<UserService> _logger;


        public UserService(ILogger<UserService> logger, IDatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        public bool CheckUserId(string id)
        {
            try
            {
                User user = _databaseContext.FindUser(ObjectId.Parse(id));

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
                User user = _databaseContext.FindUser(ObjectId.Parse(id));

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
            catch
            {
                // Id which cannot be parsed are also invalid
                return false;
            }
        }

        public async Task<string> RegisterAsync(User user)
        {
            try
            {
                var oldUser = _databaseContext.FindUser(user.Email);

                if (oldUser == null)
                {
                    await _databaseContext.InsertUserAsync(user);
                }
                else
                {
                    user.Id = oldUser.Id;
                    await _databaseContext.ReplaceUserAsync(oldUser.Id, user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
