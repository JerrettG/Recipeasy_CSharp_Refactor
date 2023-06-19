using System;
namespace Recipeasy_CSharp_Refactor.Exceptions
{
	public class FridgeItemNotFoundException : Exception
	{ 

		public FridgeItemNotFoundException()
		{
		}
		public FridgeItemNotFoundException(String message)
			: base(message)
		{
		}
	}
}

