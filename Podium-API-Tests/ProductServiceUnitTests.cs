using NUnit.Framework;
using Podium_API.Services;
using Podium_API.Models;
using Moq;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Podium_API_Tests
{
    public class ProductServiceUnitTests
    {
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            List<Product> initialData = new List<Product>()
            {
                new Product("Bank A", 2, "Variable", 60),
                new Product("Bank B", 3, "Fixed", 60),
                new Product("Bank C", 4, "Variable", 90)
            };

            var dbContextMock = new Mock<IDatabaseContext>();
            var loggerMock = new Mock<ILogger<ProductService>>();
            var taskMock = new Mock<Task<List<Product>>>();

            dbContextMock.Setup(db => db.FindProductsAsync(It.IsAny<decimal>())).ReturnsAsync((decimal requiredLTV) => initialData.FindAll(x => x.MinimumLTV > requiredLTV));

            _productService = new ProductService(loggerMock.Object, dbContextMock.Object);
        }

        [Test]
        public async Task AvailableProductsTest1Async()
        {
            List<Product> expectedProducts = new List<Product>()
            {
                new Product("Bank A", 2, "Variable", 60),
                new Product("Bank B", 3, "Fixed", 60),
                new Product("Bank C", 4, "Variable", 90)
            };

            var res = await _productService.FindAvailableProductsAsync(250000, 120000); // LTV = 52

            Assert.AreEqual(res.Count, expectedProducts.Count);

            for (int i=0; i<expectedProducts.Count; i++)
            {
                Assert.True(res[i].Equals(expectedProducts[i]));
            }
        }

        [Test]
        public async Task AvailableProductsTest2Async()
        {
            List<Product> expectedProducts = new List<Product>()
            {
                new Product("Bank C", 4, "Variable", 90)
            };

            var res = await _productService.FindAvailableProductsAsync(250000, 100000); // LTV = 60
            Assert.AreEqual(res.Count, expectedProducts.Count);

            for (int i = 0; i < expectedProducts.Count; i++)
            {
                Assert.True(res[i].Equals(expectedProducts[i]));
            }
        }

        [Test]
        public async Task AvailableProductsTest3Async()
        {
            List<Product> expectedProducts = new List<Product>();

            var res = await _productService.FindAvailableProductsAsync(250000, 25000); // LTV = 90

            Assert.AreEqual(res.Count, expectedProducts.Count);
        }
    }
}