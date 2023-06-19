using System;
using Amazon.DynamoDBv2.DataModel;
using Recipeasy_CSharp_Refactor.Repositories.Models;

namespace Recipeasy_CSharp_Refactor.Controllers.Models
{
	public class RecipeResponse
	{
    
        public String Id { get; set; }
        public String Name { get; set; }
        public List<IngredientResponse> Ingredients { get; set; }
        public int ReadyInMinutes { get; set; }
        public List<InstructionResponse> Instructions { get; set; }
        public String ImageUrl { get; set; }
        public String ImageSourceUrl { get; set; }
        public String Summary { get; set; }

        public RecipeResponse(
            String id,
            String name,
            List<IngredientResponse> ingredients,
            int readyInMinutes,
            List<InstructionResponse> instructions,
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

