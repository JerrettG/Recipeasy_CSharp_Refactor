using Bogus;
using Moq;
using Newtonsoft.Json;
using Recipeasy_CSharp_Refactor;
using Recipeasy_CSharp_Refactor.Repositories;
using Recipeasy_CSharp_Refactor.Repositories.Models;
using Recipeasy_CSharp_Refactor.Services;
using Recipeasy_CSharp_Refactor.Services.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipeasy_CSharp_RefactorTests;

public class Tests
{
    private Faker faker = new Faker();
    private Mock<FridgeItemRepository> _fridgeItemRepository;
    private Mock<IDatabase> _cache;
    private FridgeService _fridgeService;

    [SetUp]
    public void Setup()
    {
        _fridgeItemRepository = new Mock<FridgeItemRepository>();
        _cache = new Mock<IDatabase>();
        _fridgeService = new FridgeService(_cache.Object, _fridgeItemRepository.Object);
    }

    [Test]
    public void GetFridgeItemsByUserId_WithValidIdCacheMiss_ReturnsFridgeItems()
    {
        String userId = faker.Random.Word();

        List<FridgeItemEntity> expectedFridgeItems = TestUtil.CreateMockList<FridgeItemEntity>();

        _cache.Setup(
            cache => cache.StringGet(
                It.IsAny<String>(),
                It.IsAny<CommandFlags>()))
            .Returns(null as String);
        _fridgeItemRepository.Setup(repos => repos.GetFridgeItemsByUserId(userId)).Returns(expectedFridgeItems);

        List<FridgeItem> actualFridgeItems = _fridgeService.GetFridgeItemsByUserId(userId);

        for (int i = 0; i < expectedFridgeItems.Count; i++)
        {
            FridgeItemEntity expectedFridgeItem = expectedFridgeItems[i];
            FridgeItem actualFridgeItem = actualFridgeItems[i];
            Assert.That(expectedFridgeItem.UserId, Is.EqualTo(actualFridgeItem.UserId));
            Assert.That(expectedFridgeItem.Name, Is.EqualTo(actualFridgeItem.Name));
            Assert.That(expectedFridgeItem.PurchaseDate, Is.EqualTo(actualFridgeItem.PurchaseDate));
            Assert.That(expectedFridgeItem.LastUpdated, Is.EqualTo(actualFridgeItem.LastUpdated));
            Assert.That(expectedFridgeItem.Quantity, Is.EqualTo(actualFridgeItem.Quantity));
            Assert.That(expectedFridgeItem.UnitMeasurement, Is.EqualTo(actualFridgeItem.UnitMeasurement));
        }
    }
}
