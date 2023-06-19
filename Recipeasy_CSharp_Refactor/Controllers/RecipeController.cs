using Microsoft.AspNetCore.Mvc;
using Recipeasy_CSharp_Refactor.Controllers.Models;
using Recipeasy_CSharp_Refactor.Exceptions;
using Recipeasy_CSharp_Refactor.Services;
using Recipeasy_CSharp_Refactor.Services.Models;
using Recipeasy_CSharp_Refactor.Utils;

namespace Recipeasy_CSharp_Refactor.Controllers;

[ApiController]
[Route("api/v1/recipeService")]

public class RecipeController : ControllerBase
{
    private readonly RecipeService _recipeService;

    public RecipeController(RecipeService recipeService)
	{
		_recipeService = recipeService;
	}

	[HttpGet]
	[Route("/recipes")]
	public ActionResult<IEnumerable<RecipeResponse>> GetRecipesUsingComplexSearch()
	{
		var queryParams = GetQueryParams(HttpContext.Request.Query);

		try
		{
			IEnumerable<Recipe> recipes = _recipeService
				.GetRecipesUsingComplexSearch(queryParams);
			IEnumerable<RecipeResponse> recipeResponses = recipes.Select(recipe => ModelConverter.ConvertToResponse(recipe)).ToList();
			return Ok(recipeResponses);
		} catch (ApiGatewayException e)
		{
			return StatusCode(e.StatusCode);
		}
	}


	private Dictionary<String, String> GetQueryParams(IQueryCollection queryParams)
	{
        Dictionary<String, String> parameters = new Dictionary<String, String>();

        foreach (var param in queryParams)
        {
            parameters[param.Key] = param.Value!;
        }

		return parameters;
    }
}

