using NUnit.Framework;
using Podium_API.Services;
using Podium_API.Models;
using Moq;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace Podium_API_Tests
{
    public class UserServiceUnitTests
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            User testUser = new User();
            testUser.FirstName = "Thomas";
            testUser.LastName = "Varghese";
            testUser.Email = "test@test.com";
            testUser.DateOfBirth = DateTime.Parse("1993-08-10");
            testUser.Id = ObjectId.Parse("601ae6981612b057d41504dd");

            var dbContextMock = new Mock<IDatabaseContext>();
            var loggerMock = new Mock<ILogger<UserService>>();


            dbContextMock.Setup(db => db.FindUser(testUser.Id)).Returns(testUser);
            dbContextMock.Setup(db => db.FindUser(testUser.Email)).Returns(testUser);

            _userService = new UserService(loggerMock.Object, dbContextMock.Object);
        }

        [Test]
        public void InvalidUserIDTest1()
        {
            var res =_userService.CheckUserId("xxxxxxxxxxxx");
            Assert.False(res);
        }

        [Test]
        public void InvalidUserIDTest2()
        {
            var res = _userService.CheckUserId("601ae6981612b057d41503dd");
            Assert.False(res);
        }

        [Test]
        public void ValidUserIDTest()
        {
            var res = _userService.CheckUserId("601ae6981612b057d41504dd");
            Assert.True(res);
        }

        [Test]
        public void InvalidAge()
        {
            var res = _userService.ValidAge("601ae6981612b057d41504dd");
            Assert.True(res);
        }

        [Test]
        public async Task ReregisterWithSameEmail_ReturnSameIDAsync()
        {
            User testUser = new User();
            testUser.FirstName = "Varghese";
            testUser.LastName = "Thomas";
            testUser.Email = "test@test.com";
            testUser.DateOfBirth = DateTime.Parse("1993-08-10");

            var res = await _userService.RegisterAsync(testUser);
            Assert.AreEqual("601ae6981612b057d41504dd", res);
        }
    }
}