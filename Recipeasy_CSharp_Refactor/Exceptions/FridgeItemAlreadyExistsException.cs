using System;
namespace Recipeasy_CSharp_Refactor.Exceptions
{
	public class FridgeItemAlreadyExistsException : Exception
	{
		public FridgeItemAlreadyExistsException()
		{
		}
		public FridgeItemAlreadyExistsException(String message)
			: base(message)
		{
		}
	}
}

