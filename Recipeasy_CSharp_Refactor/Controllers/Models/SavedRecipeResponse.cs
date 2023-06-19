using System;
using Recipeasy_CSharp_Refactor.Services.Models;

namespace Recipeasy_CSharp_Refactor.Controllers.Models
{
	public class SavedRecipeResponse
	{
        public String UserId { get; set; }
        public int RecipeId { get; set; }
        public String Name { get; set; }
        public List<IngredientResponse> Ingredients { get; set; }
        public int ReadyInMinutes { get; set; }
        public List<InstructionResponse> Instructions { get; set; }
        public String ImageUrl { get; set; }
        public String ImageSourceUrl { get; set; }
        public String Summary { get; set; }

        public SavedRecipeResponse(
                String userId,
                int recipeId,
                String name,
                List<IngredientResponse> ingredients,
                int readyInMinutes,
                List<InstructionResponse> instructions,
                String imageUrl,
                String imageSourceUrl,
                String summary
            )
        {
            UserId = userId;
            RecipeId = recipeId;
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

