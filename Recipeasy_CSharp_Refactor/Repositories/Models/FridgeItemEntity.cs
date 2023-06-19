using System;
using Amazon.DynamoDBv2.DataModel;

namespace Recipeasy_CSharp_Refactor.Repositories.Models
{
	[DynamoDBTable("FridgeItems")]
	public class FridgeItemEntity
	{

		[DynamoDBHashKey("user_id")]
		public String UserId { get; set; }

		[DynamoDBRangeKey("name")]
		public String Name { get; set; }

		[DynamoDBProperty("purchase_date")]
		public String? PurchaseDate { get; set; }

		[DynamoDBProperty("last_updated")]
		public String? LastUpdated { get; set; }

		[DynamoDBProperty("quantity")]
		public double? Quantity { get; set; }

		[DynamoDBProperty("unit_measurement")]
		public String? UnitMeasurement { get; set; }

		[DynamoDBProperty("image_file_name")]
		public String? ImageFileName { get; set; }

		public FridgeItemEntity(
				String userId,
				String name,
				String? purchaseDate,
				String? lastUpdated,
				double? quantity,
				String? unitMeasurement,
				String? imageFileName
			)
		{
			UserId = userId;
			Name = name;
			PurchaseDate = purchaseDate;
			LastUpdated = lastUpdated;
			Quantity = quantity;
			UnitMeasurement = unitMeasurement;
			ImageFileName = imageFileName;
		}
	}
}

