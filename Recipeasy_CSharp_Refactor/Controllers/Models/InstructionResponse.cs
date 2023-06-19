using System;
namespace Recipeasy_CSharp_Refactor.Controllers.Models
{
	public class InstructionResponse
	{
        public int Number { get; set; }
        public String Step { get; set; }

        public InstructionResponse(int number, String step)
        {
            Number = number;
            Step = step;
        }
    }
}

