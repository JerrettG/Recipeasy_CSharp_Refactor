using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;

namespace Recipeasy_CSharp_Refactor.Repositories.Models
{
	public class InstructionEntity
	{
		
		public int Number { get; set; }
		public String Step { get; set; }


		public InstructionEntity(int number, String step)
		{
			Number = number;
			Step = step;
		}

	}

}

