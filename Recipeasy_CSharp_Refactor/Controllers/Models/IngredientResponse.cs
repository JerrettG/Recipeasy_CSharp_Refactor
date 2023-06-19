using System;
namespace Recipeasy_CSharp_Refactor.Controllers.Models
{
	public class IngredientResponse
	{
		public int Id { get; set; }
		public String Name { get; set; }
		public double Quantity { get; set; }
		public String UnitMeasurement { get; set; }
		public String ImageFileName { get; set; }

		public IngredientResponse(int id, String name, double quantity,
			String unitMeasurement, String imageFileName)
		{
			Id = id;
			Name = name;
			Quantity = quantity;
			UnitMeasurement = unitMeasurement;
			ImageFileName = imageFileName;
		}
	}
}

