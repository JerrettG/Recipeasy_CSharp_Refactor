using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Recipeasy_CSharp_Refactor.Repositories.Models;
using System;

namespace Recipeasy_CSharp_Refactor.Repositories
{
	public class SavedRecipeRepository
	{
		private readonly DynamoDBContext _ddbContext;

		public SavedRecipeRepository(DynamoDBContext ddbContext)
		{
			_ddbContext = ddbContext;
		}

		public List<SavedRecipeEntity> GetSavedRecipesByUserId(String userId)
		{
            var queryConfig = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
				{
					new ScanCondition("user_id", ScanOperator.Equal, userId)
				}
            };

            return _ddbContext.QueryAsync<SavedRecipeEntity>(userId, queryConfig)
				.GetRemainingAsync()
				.Result;
		}

		public SavedRecipeEntity GetSavedRecipe(String userId, int recipeId)
		{
			return _ddbContext.LoadAsync<SavedRecipeEntity>(userId, recipeId).Result;
		}

		public Boolean ExistsById(String userId, int recipeId)
		{
			return _ddbContext.LoadAsync<SavedRecipeEntity>(userId, recipeId).Result == null;
		}

        /*
		 * This method saves a SavedRecipeEntity to DynamoDB. If the
		 * item already exists, the item in the database will be updated and 
		 * any null attributes will remain the same.
		 */
        public async void SaveSavedRecipe(SavedRecipeEntity entity)
		{
			await _ddbContext.SaveAsync<SavedRecipeEntity>(entity);
		}

		public async void RemoveSavedRecipe(String userId, int recipeId)
		{
			await _ddbContext.DeleteAsync<SavedRecipeEntity>(userId, recipeId);
		}
	}
}

