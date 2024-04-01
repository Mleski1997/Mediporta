using Castle.Core.Logging;
using Mediporta.Api.Data;
using Mediporta.Api.Models;
using Mediporta.Api.Repository;
using Mediporta.Api.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediportaTests.Mediporta.Test.Unit
{
    public class ItemSerivceTests
    {
        [Fact]
        public async Task PercentCountTest()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(repo => repo.GetItemsFromDB())
                .ReturnsAsync(new List<Item>
                {
                new Item { Id = 1, Name = "Item1", Count = 50 },
                new Item { Id = 2, Name = "Item2", Count = 50 }
                });
            var loggerMock = new Mock<ILogger<ItemService>>();
            var service = new ItemService(mockRepo.Object, loggerMock.Object);

            var result = await service.PercentCount();

            Assert.Equal(2, result.Count());
            Assert.Equal(50, result.First().Percent);
            Assert.Equal(50, result.Last().Percent);
        }



        [Fact]
        public async Task AddItemTest()
        {
            var mockRepo = new Mock<IItemRepository>();
            var loggerMock = new Mock<ILogger<ItemService>>();

            var itemsToAdd = new List<Item>
             {
                new Item { Name = "Test1", Count = 50 },
                new Item { Name = "Test2", Count = 10 },
                new Item { Name = "Test3", Count = 40 }
                };


            var service = new ItemService(mockRepo.Object, loggerMock.Object);


            await service.AddAsync(itemsToAdd);


            mockRepo.Verify(repo => repo.AddAsync(itemsToAdd), Times.Once);
        }
    }
}