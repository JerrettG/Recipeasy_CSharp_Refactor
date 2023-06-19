using System;
namespace Recipeasy_CSharp_Refactor.Services.Models
{
	public class Ingredient
	{
        public int Id { get; set; }
        public String Name { get; set; }
        public double? Quantity { get; set; }
        public String? UnitMeasurement { get; set; }
        public String? ImageFileName { get; set; }

        public Ingredient(
            int id,
            String name,
            double? quantity,
            String? unitMeasurement,
            String? imageFileName)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            UnitMeasurement = unitMeasurement;
            ImageFileName = imageFileName;
        }
    }
}

