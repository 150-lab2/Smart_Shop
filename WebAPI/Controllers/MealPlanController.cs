using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SmartShopContext _dbContext;

        public MealPlanController(IHttpClientFactory httpClientFactory, SmartShopContext dbContext)
        {
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
        }


        public async Task<ActionResult<Guid>> StartNew(Guid userId, DateOnly starting, DateOnly ending)
        {
            // Psudo: 1. Check that the user doesn't already have a meal plan for the same date range
            //        2. Create the meal plan record, add and save.


            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a recipe to a meal plan
        /// </summary>
        /// <param name="mealPlanId"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddRecipe(Guid mealPlanId, string recipeId)
        {
            // Psudo: 1. Get the meal plan. 2. If the recipe isn't already added, add the recipe

            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a recipe from a meal plan
        /// </summary>
        /// <param name="mealPlanId"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteRecipe(Guid mealPlanId, string recipeId)
        {
            // Psudo: 1. Get the meal plan, 2. remove the recipe from the the associated collection, 3. Save

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a json array of the recipies included in the meal plan.
        /// </summary>
        /// <param name="mealPlanId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> GetRecipe(Guid mealPlanId)
        {
            // Psudo: 1. Get the meal plan associated recipies, 2. Query spoontacular for the recipe details, 3. return the recpies as a json array

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a shopping list for the meal plan
        /// </summary>
        /// <param name="mealPlanId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> ShoppingList(Guid mealPlanId)
        {
            // Psudo:  1. Get all the recipies, 2. Collect and consolidate ingredients, 3. Return the array

            throw new NotImplementedException();
        }


    }
}
