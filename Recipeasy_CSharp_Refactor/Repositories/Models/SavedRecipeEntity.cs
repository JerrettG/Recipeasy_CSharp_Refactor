using System;
using Amazon.DynamoDBv2.DataModel;

namespace Recipeasy_CSharp_Refactor.Repositories.Models
{
    [DynamoDBTable("SavedRecipes")]
    public class SavedRecipeEntity
    {

        [DynamoDBHashKey("user_id")]
        public String UserId { get; set; }
        [DynamoDBRangeKey("recipe_id")]
        public int RecipeId { get; set; }
        [DynamoDBProperty("name")]
        public String Name { get; set; }

        [DynamoDBProperty("ingredients",typeof(IngredientEntityConverter))]
        public List<IngredientEntity> Ingredients { get; set; }

        [DynamoDBProperty("ready_in_minutes")]
        public int ReadyInMinutes { get; set; }

        [DynamoDBProperty("instructions",typeof(InstructionEntityConverter))]
        public List<InstructionEntity> Instructions { get; set; }

        [DynamoDBProperty("image_url")]
        public String ImageUrl { get; set; }

        [DynamoDBProperty("image_source_url")]
        public String ImageSourceUrl { get; set; }

        [DynamoDBProperty("summary")]
        public String Summary { get; set; }

        public SavedRecipeEntity(
            String userId,
            int recipeId,
            String name,
            List<IngredientEntity> ingredients,
            int readyInMinutes,
            List<InstructionEntity> instructions,
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

