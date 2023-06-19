using Recipeasy_CSharp_Refactor.Exceptions;
using Recipeasy_CSharp_Refactor.Repositories;
using Recipeasy_CSharp_Refactor.Repositories.Models;
using Recipeasy_CSharp_Refactor.Services.Models;
using Recipeasy_CSharp_Refactor.Utils;
using System;
using StackExchange.Redis;

namespace Recipeasy_CSharp_Refactor.Services
{
	public class FridgeService
	{
        private readonly static String FRIDGE_ITEMS_BY_USERID_CACHE_KEY = "FridgeItemsByUserId:{0}";
        private readonly IDatabase _cache;
        private readonly FridgeItemRepository _fridgeItemRepository;

        public FridgeService(
            IDatabase cache,
            FridgeItemRepository fridgeItemRepository)
        {
            _cache = cache;
            _fridgeItemRepository = fridgeItemRepository;
        }

        public List<FridgeItem> GetFridgeItemsByUserId(String userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException(
                    "Cannot retrieve fridge items for null or blank user id.");
            }

            String cacheKey = String.Format(FRIDGE_ITEMS_BY_USERID_CACHE_KEY, userId);

            String? cachedFridgeItemsAsJson = _cache.StringGet(cacheKey);

            if (!String.IsNullOrEmpty(cachedFridgeItemsAsJson))
            {
                return ModelConverter.FromJson<List<FridgeItem>>(cachedFridgeItemsAsJson);
            }

            List<FridgeItemEntity> fridgeItemEntities = _fridgeItemRepository
                .GetFridgeItemsByUserId(userId);

            List<FridgeItem> fridgeItems = fridgeItemEntities?
                .Select(entity => ModelConverter.ConvertFromEntity(entity))
                .ToList() ?? new List<FridgeItem>();

            _cache.StringSet(cacheKey, ModelConverter.ToJson(fridgeItems));

            return fridgeItems;
        }

        public FridgeItem AddFridgeItem(FridgeItem fridgeItem)
        {
            if (fridgeItem == null)
            {
                throw new ArgumentException(
                    "Cannot add fridge item when fridge item is null.");
            }

            String userId = fridgeItem.UserId;
            String name = fridgeItem.Name;

            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentException(
                    "Cannot add fridge item for null or blank user id.");
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException(
                    "Cannot add fridge item for null or blank item name.");
            }

            if (_fridgeItemRepository.ExistsById(userId, name))
            {
                throw new FridgeItemAlreadyExistsException(
                    String.Format(
                        "Cannot create fridge item with userId: {0} and name: {1}",
                        userId,
                        name));
            }

            fridgeItem.LastUpdated = DateTime.Now.ToString();

            FridgeItemEntity fridgeItemEntity = ModelConverter.ConvertToEntity(fridgeItem);

            _fridgeItemRepository.SaveFridgeItem(fridgeItemEntity);

            String cacheKey = String.Format(FRIDGE_ITEMS_BY_USERID_CACHE_KEY, userId);

            _cache.KeyDelete(cacheKey);

            return fridgeItem;
        }

        public FridgeItem UpdateFridgeItem(FridgeItem fridgeItem)
        {
            if (fridgeItem == null)
            {
                throw new ArgumentException(
                    "Cannot update fridge item when fridge item is null.");
            }

            String userId = fridgeItem.UserId;
            String name = fridgeItem.Name;

            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentException(
                    "Cannot update fridge item for null or blank user id.");
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException(
                    "Cannot update fridge item for null or blank item name.");
            }
            if (fridgeItem.Quantity <= 0)
            {
                throw new ArgumentException(
                    "Cannot update fridge item with quantity <= 0.");
            }

            if (!_fridgeItemRepository.ExistsById(userId, name))
            {
                throw new FridgeItemNotFoundException(
                    String.Format(
                        "Cannot update fridge item with userId: {0} and name: {1}. Item not found.",
                        userId,
                        name));
            }

            fridgeItem.LastUpdated = DateTime.Now.ToString();

            _fridgeItemRepository.SaveFridgeItem(ModelConverter.ConvertToEntity(fridgeItem));

            String cacheKey = String.Format(FRIDGE_ITEMS_BY_USERID_CACHE_KEY, userId);
            _cache.KeyDelete(cacheKey);

            return fridgeItem;
        }

        public FridgeItem RemoveFridgeItem(String userId, String name)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentException(
                    "Cannot remove fridge item with null or blank user id.");
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException(
                    "Cannot remove fridgeItem with null of blank item name.");
            }

            FridgeItemEntity existingFridgeItem = _fridgeItemRepository.GetFridgeItem(userId, name);

            if (existingFridgeItem == null)
            {
                throw new FridgeItemNotFoundException(
                    String.Format(
                        "Cannot remove fridge item with userId: {0} and name: {1}. Item not found.",
                        userId,
                        name));
            }

            _fridgeItemRepository.RemoveFridgeItem(userId, name);

            String cacheKey = String.Format(FRIDGE_ITEMS_BY_USERID_CACHE_KEY, userId);
            _cache.KeyDelete(cacheKey);

            return ModelConverter.ConvertFromEntity(existingFridgeItem);
        }

    }
}

