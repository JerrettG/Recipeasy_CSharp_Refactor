using System;
namespace Recipeasy_CSharp_Refactor.Exceptions
{
	public class SavedRecipeAlreadyExistsException : Exception
	{

		public SavedRecipeAlreadyExistsException()
		{
		}
		public SavedRecipeAlreadyExistsException(String message)
			: base(message)
		{
		}
	}
}

