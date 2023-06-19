using System;
namespace Recipeasy_CSharp_Refactor.Exceptions
{
	public class SavedRecipeNotFoundException : Exception
	{

		public SavedRecipeNotFoundException()
		{
		}
		public SavedRecipeNotFoundException(String message)
			: base(message)
		{
		}
	}
}

