using Newtonsoft.Json;
using System;
using Recipeasy_CSharp_Refactor.Controllers.Models;
using Recipeasy_CSharp_Refactor.Services.Models;
using Recipeasy_CSharp_Refactor.Repositories.Models;
using System.Text;

namespace Recipeasy_CSharp_Refactor.Utils
{
	public class ModelConverter
	{
		public ModelConverter()
		{
		}

		public static string ConvertToCsvString<T>(List<T> list, Func<T, string> stringFunction)
		{
			StringBuilder csvBuilder = list.Aggregate(
				new StringBuilder(),
				(sb, item) => sb
					.Append(stringFunction(item))
					.Append(',')
			);

			if (csvBuilder.Length > 0)
			{
				csvBuilder.Length--; // remove the trailing comma
			}

			return csvBuilder.ToString();
		}

		public static string ToJson<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		public static T FromJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json)!;
		}

    public static RecipeResponse ConvertToResponse(Recipe recipe)
		{
			return new RecipeResponse(
					recipe.Id,
					recipe.Name,
					recipe.Ingredients
						.Select(ingr => ModelConverter.ConvertToResponse(ingr))
						.ToList(),
					recipe.ReadyInMinutes,
					recipe.Instructions
						.Select(instr => ModelConverter.ConvertToResponse(instr))
						.ToList(),
					recipe.ImageUrl,
					recipe.ImageSourceUrl,
					recipe.Summary
				) ;
		}

		public static IngredientResponse ConvertToResponse(Ingredient ingredient)
		{
			return new IngredientResponse(
					ingredient.Id,
					ingredient.Name,
					ingredient.Quantity!.Value!,
					ingredient.UnitMeasurement!,
					ingredient.ImageFileName!
				);
		}

		public static InstructionResponse ConvertToResponse(Instruction instruction)
		{
			return new InstructionResponse(
					instruction.Number,
					instruction.Step
				);
		}

		public static FridgeItemResponse ConvertToResponse(FridgeItem fridgeItem)
		{
			return new FridgeItemResponse(
					fridgeItem.UserId,
					fridgeItem.Name,
					fridgeItem.PurchaseDate!,
					fridgeItem.LastUpdated!,
					fridgeItem.Quantity!.Value!,
					fridgeItem.UnitMeasurement!,
					fridgeItem.ImageFileName!
				);
		}

		public static SavedRecipeResponse ConvertToResponse(SavedRecipe savedRecipe)
		{
			return new SavedRecipeResponse(
					savedRecipe.UserId,
					savedRecipe.RecipeId,
					savedRecipe.Name,
					savedRecipe.Ingredients
							.Select(ingr => ModelConverter.ConvertToResponse(ingr))
							.ToList(),
					savedRecipe.ReadyInMinutes,
					savedRecipe.Instructions
							.Select(instr => ModelConverter.ConvertToResponse(instr))
							.ToList(),
					savedRecipe.ImageUrl,
					savedRecipe.ImageSourceUrl,
					savedRecipe.Summary
				);
		}

		public static FridgeItemEntity ConvertToEntity(FridgeItem fridgeItem)
		{
			return new FridgeItemEntity(
					fridgeItem.UserId,
					fridgeItem.Name,
					fridgeItem.PurchaseDate,
					fridgeItem.LastUpdated,
					fridgeItem.Quantity,
					fridgeItem.UnitMeasurement,
					fridgeItem.ImageFileName
				);
		}

		public static IngredientEntity ConvertToEntity(Ingredient ingredient)
		{
			return new IngredientEntity(
					ingredient.Id,
					ingredient.Name,
					ingredient.Quantity,
					ingredient.UnitMeasurement,
					ingredient.ImageFileName
				);
		}

		public static InstructionEntity ConvertToEntity(Instruction instruction)
		{
			return new InstructionEntity(
					instruction.Number,
					instruction.Step
				);
		}

		public static SavedRecipeEntity ConvertToEntity(SavedRecipe savedRecipe)
		{
			return new SavedRecipeEntity(
					savedRecipe.UserId,
					savedRecipe.RecipeId,
					savedRecipe.Name,
					savedRecipe.Ingredients
						.Select(ingr => ModelConverter.ConvertToEntity(ingr))
						.ToList(),
					savedRecipe.ReadyInMinutes,
					savedRecipe.Instructions
						.Select(instr => ModelConverter.ConvertToEntity(instr))
						.ToList(),
					savedRecipe.ImageUrl,
					savedRecipe.ImageSourceUrl,
					savedRecipe.Summary
				);
		}

		public static FridgeItem ConvertFromEntity(FridgeItemEntity entity)
		{
			return new FridgeItem(
					entity.UserId,
					entity.Name,
					entity.PurchaseDate,
					entity.LastUpdated,
					entity.Quantity,
					entity.UnitMeasurement,
					entity.ImageFileName
				);
		}

		public static Ingredient ConvertFromEntity(IngredientEntity entity)
		{
			return new Ingredient(
					entity.Id,
					entity.Name,
					entity.Quantity,
					entity.UnitMeasurement,
					entity.ImageFileName
				);
		}

		public static Instruction ConvertFromEntity(InstructionEntity entity)
		{
			return new Instruction(
					entity.Number,
					entity.Step
				);
		}

		public static SavedRecipe ConvertFromEntity(SavedRecipeEntity entity)
		{
			return new SavedRecipe(
					entity.UserId,
					entity.RecipeId,
					entity.Name,
					entity.Ingredients
						.Select(ingr => ModelConverter.ConvertFromEntity(ingr))
						.ToList(),
					entity.ReadyInMinutes,
					entity.Instructions
						.Select(instr => ModelConverter.ConvertFromEntity(instr))
						.ToList(),
					entity.ImageUrl,
					entity.ImageSourceUrl,
					entity.Summary
				);
		}
	}
}

