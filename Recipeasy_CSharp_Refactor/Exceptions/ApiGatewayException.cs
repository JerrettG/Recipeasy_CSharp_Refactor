using System;
namespace Recipeasy_CSharp_Refactor.Exceptions
{
	public class ApiGatewayException : Exception
	{
		public int StatusCode { get; }

		public ApiGatewayException()
		{
		}

		public ApiGatewayException(int statusCode, String message)
			: base(message)
		{
			StatusCode = statusCode;
		}
	}
}

