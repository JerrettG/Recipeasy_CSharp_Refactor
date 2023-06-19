using Amazon.DynamoDBv2.DataModel;
using Recipeasy_CSharp_Refactor.Repositories.Models;
using System;

namespace Recipeasy_CSharp_Refactor.Repositories
{
	public class FridgeItemRepository
	{
		private readonly DynamoDBContext? _ddbContext;

		public FridgeItemRepository()
		{

		}
		public FridgeItemRepository(DynamoDBContext ddbContext)
		{
			_ddbContext = ddbContext;
		}

		public List<FridgeItemEntity> GetFridgeItemsByUserId(String userId)
		{
			var queryConfig = new DynamoDBOperationConfig()
			{
				QueryFilter = new List<ScanCondition>()
				{
					new ScanCondition("user_id", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, userId)
				}
			};

			return _ddbContext.QueryAsync<FridgeItemEntity>(userId, queryConfig)
				.GetRemainingAsync()
				.Result;
		}

		public FridgeItemEntity GetFridgeItem(String userId, String name)
		{
			return _ddbContext.LoadAsync<FridgeItemEntity>(userId, name).Result;
		}

		public Boolean ExistsById(String userId, String name)
		{
			return _ddbContext.LoadAsync<FridgeItemEntity>(userId, name).Result == null;
		}

		/*
		 * This method is used to save a FridgeItemEntity to DynamoDB. If the
		 * item already exists, the item in the database will be updated and 
		 * any null attributes will remain the same.
		 */
		public void SaveFridgeItem(FridgeItemEntity entity)
		{
			_ddbContext.SaveAsync<FridgeItemEntity>(entity);
		}

		public void RemoveFridgeItem(String userId, String name)
		{
			_ddbContext.DeleteAsync<FridgeItemEntity>(userId, name);
		}
	}
}

