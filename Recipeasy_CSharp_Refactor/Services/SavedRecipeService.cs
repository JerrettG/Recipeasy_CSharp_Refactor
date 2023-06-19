using Recipeasy_CSharp_Refactor.Exceptions;
using Recipeasy_CSharp_Refactor.Repositories;
using Recipeasy_CSharp_Refactor.Repositories.Models;
using Recipeasy_CSharp_Refactor.Services.Models;
using Recipeasy_CSharp_Refactor.Utils;
using StackExchange.Redis;
using System;
namespace Recipeasy_CSharp_Refactor.Services
{
	public class SavedRecipeService
	{
		private readonly SavedRecipeRepository _savedRecipeRepository;
		private readonly IDatabase _cache;
		private readonly String SAVED_RECIPES_BY_USERID_CACHE_KEY = "SavedRecipesByUserId::{0}";

		public SavedRecipeService(
			IDatabase cache,
			SavedRecipeRepository savedRecipeRepository)
		{
			_cache = cache;
			_savedRecipeRepository = savedRecipeRepository;
		}

		public List<SavedRecipe> GetSavedRecipesByUserId(String userId)
		{
            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException(
					"Cannot retrieve saved recipes for null or empty user id");
            }

            String cacheKey = String.Format(SAVED_RECIPES_BY_USERID_CACHE_KEY, userId);

			String? cachedRecipesAsJson = _cache.StringGet(cacheKey);

			if (!String.IsNullOrEmpty(cachedRecipesAsJson))
			{
				return ModelConverter.FromJson<List<SavedRecipe>>(cachedRecipesAsJson);
			}

			List<SavedRecipeEntity> recipeEntities = _savedRecipeRepository
														.GetSavedRecipesByUserId(userId);
			List<SavedRecipe> savedRecipes = recipeEntities?
				.Select(entity => ModelConverter.ConvertFromEntity(entity))
				.ToList() ?? new List<SavedRecipe>();

			String savedRecipesAsJson = ModelConverter.ToJson(savedRecipes);
            _cache.StringSet(cacheKey, savedRecipesAsJson);

			return savedRecipes;
        }

		public SavedRecipe GetSavedRecipe(String userId, int recipeId)
		{
			if (String.IsNullOrEmpty(userId) || String.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException(
					"Cannot retrieve saved recipe when userId is null or blank.");
			}
			if (recipeId <= 0)
			{
				throw new ArgumentException(
					"Cannot retrieve recipe with invalid recipe id.");
			}

			SavedRecipeEntity savedRecipeEntity = _savedRecipeRepository
				.GetSavedRecipe(userId, recipeId);

			return ModelConverter.ConvertFromEntity(savedRecipeEntity);
		}

		public SavedRecipe AddSavedRecipe(SavedRecipe savedRecipe)
		{
			if (savedRecipe == null)
			{
				throw new ArgumentException(
					"Cannot add saved recipe when saved recipe is null.");
			}
			String userId = savedRecipe.UserId;
			if (String.IsNullOrEmpty(userId) || String.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException(
					"Cannot add saved recipe for null or blank user id.");
			}
			if (savedRecipe.RecipeId <= 0)
			{
				throw new ArgumentException(
					"Cannot add saved recipe for invalid recipe id.");
			}
			if (_savedRecipeRepository.ExistsById(userId, savedRecipe.RecipeId))
			{
				throw new SavedRecipeAlreadyExistsException(
					"Unable to add saved recipe. Saved recipe already exists.");
			}

			_savedRecipeRepository.SaveSavedRecipe(ModelConverter.ConvertToEntity(savedRecipe));

            String cacheKey = String.Format(SAVED_RECIPES_BY_USERID_CACHE_KEY, userId);
            _cache.KeyDelete(cacheKey);
            return savedRecipe;
		}

		public SavedRecipe RemoveSavedRecipe(String userId, int recipeId)
		{ 
            if (String.IsNullOrEmpty(userId) || String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException(
                    "Cannot remove saved recipe for null or blank user id.");
            }
            if (recipeId <= 0)
            {
                throw new ArgumentException(
                    "Cannot remove saved recipe for invalid recipe id.");
            }
			SavedRecipeEntity existingRecipe = _savedRecipeRepository.GetSavedRecipe(userId, recipeId);

            if (existingRecipe == null)
			{
				throw new SavedRecipeNotFoundException(
					String.Format("Cannot find recipe with userId: {0} and recipe id {1}.", userId, recipeId));
			}

			_savedRecipeRepository.RemoveSavedRecipe(userId, recipeId);

			String cacheKey = String.Format(SAVED_RECIPES_BY_USERID_CACHE_KEY, userId);
			_cache.KeyDelete(cacheKey);

			return ModelConverter.ConvertFromEntity(existingRecipe);

        }


	}
}

