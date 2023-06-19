using System;
namespace Recipeasy_CSharp_Refactor.Services.Models
{
	public class Instruction
	{
		public int Number { get; set; }
		public String Step { get; set; }

		public Instruction(int number, String step)
		{
			Number = number;
			Step = step;
		}
	}
}

