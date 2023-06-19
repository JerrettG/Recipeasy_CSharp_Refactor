using System;
namespace Recipeasy_CSharp_Refactor.Controllers.Models
{
	public class FridgeItemResponse
	{
        public String UserId { get; set; }
        public String Name { get; set; }
        public String PurchaseDate { get; set; }
        public String LastUpdated { get; set; }
        public double Quantity { get; set; }
        public String UnitMeasurement { get; set; }
        public String ImageFileName { get; set; }

        public FridgeItemResponse(
                String userId,
                String name,
                String purchaseDate,
                String lastUpdated,
                double quantity,
                String unitMeasurement,
                String imageFileName
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

