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
            // arrange
            User testUser = new User();
            testUser.FirstName = "Thomas";
            testUser.LastName = "Varghese";
            testUser.Email = "test@test.com";
            testUser.DateOfBirth = DateTime.Parse("1993-08-10");
            testUser.Id = ObjectId.Parse("601ae6981612b057d41504dd");

            User testUser_under18 = new User();
            testUser_under18.FirstName = "Joseph";
            testUser_under18.LastName = "Varghese";
            testUser_under18.Email = "test2@test.com";
            testUser_under18.DateOfBirth = DateTime.Parse("2010-05-10");
            testUser_under18.Id = ObjectId.Parse("601ae6981612b057d41501dd");

            var dbContextMock = new Mock<IDatabaseContext>();
            var loggerMock = new Mock<ILogger<UserService>>();


            dbContextMock.Setup(db => db.FindUser(testUser.Id)).Returns(testUser);
            dbContextMock.Setup(db => db.FindUser(testUser.Email)).Returns(testUser);

            dbContextMock.Setup(db => db.FindUser(testUser_under18.Id)).Returns(testUser_under18);
            dbContextMock.Setup(db => db.FindUser(testUser_under18.Email)).Returns(testUser_under18);

            _userService = new UserService(loggerMock.Object, dbContextMock.Object);
        }

        [Test]
        public void InvalidUserIDTest1()
        {
            // act
            var res =_userService.CheckUserId("xxxxxxxxxxxx");

            // assert
            Assert.False(res);
        }

        [Test]
        public void InvalidUserIDTest2()
        {
            // act
            var res = _userService.CheckUserId("601ae6981612b057d41503dd");

            // assert
            Assert.False(res);
        }

        [Test]
        public void ValidUserIDTest()
        {
            // act
            var res = _userService.CheckUserId("601ae6981612b057d41504dd");

            // assert
            Assert.True(res);
        }

        [Test]
        public void CheckValidAge()
        {
            // act
            var res = _userService.ValidAge("601ae6981612b057d41504dd");

            // assert
            Assert.True(res);
        }

        [Test]
        public void CheckInvalidAge()
        {
            // act
            var res = _userService.ValidAge("601ae6981612b057d41501dd");

            // assert
            Assert.False(res);
        }

        [Test]
        public async Task ReregisterWithSameEmail_ReturnSameIDAsync()
        {
            // arrange
            User testUser = new User();
            testUser.FirstName = "Varghese";
            testUser.LastName = "Thomas";
            testUser.Email = "test@test.com";
            testUser.DateOfBirth = DateTime.Parse("1993-08-10");

            // act
            var res = await _userService.RegisterAsync(testUser);

            // assert
            Assert.AreEqual("601ae6981612b057d41504dd", res);
        }
    }
}