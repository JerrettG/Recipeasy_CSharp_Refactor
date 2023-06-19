using System;
using Recipeasy_CSharp_Refactor.Controllers.Models;

namespace Recipeasy_CSharp_Refactor.Services.Models
{
	public class Recipe
	{
        public String Id { get; set; }
        public String Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int ReadyInMinutes { get; set; }
        public List<Instruction> Instructions { get; set; }
        public String ImageUrl { get; set; }
        public String ImageSourceUrl { get; set; }
        public String Summary { get; set; }

        public Recipe(
            String id,
            String name,
            List<Ingredient> ingredients,
            int readyInMinutes,
            List<Instruction> instructions,
            String imageUrl,
            String imageSourceUrl,
            String summary
        )
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
            ReadyInMinutes = readyInMinutes;
            Instructions = instructions;
            ImageUrl = imageUrl;
            ImageSourceUrl = imageSourceUrl;
            Summary = summary;
        }
    }
}

