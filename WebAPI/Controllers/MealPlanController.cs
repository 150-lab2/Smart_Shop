﻿using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net.Mail;
using System.Text.Json;

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

            if (_dbContext.Set<MealPlan>().Any(m => m.StartDate <= ending && m.EndDate >= starting))
                return BadRequest("A meal plan already exists that covers this date range.");
            else{
                return Ok();
            }

            var newPlan = MealPlan.Create(userId, starting, ending);
            _dbContext.Set<MealPlan>().Add(newPlan);

            return Ok(newPlan.Id);
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

            var mealPlan = _dbContext
                .Set<MealPlan>()
                .Include(m => m.Recipes)
                .SingleOrDefault(m => m.Id == mealPlanId);

            if (mealPlan == null)
                return NotFound();

            if(mealPlan.Recipes.Any(m=>m.ApiId == recipeId) ){
                return BadRequest("recipe already exists in the meal plan");
            }
            else {
                // TODO: If the recipie properties isn't already in our database, get the recipe from spoontacular and add it, then add
                //       the reference
                //var recipie = Recipe.Create(mealPlanId, recipeId, "Name From Spoontacular");            
                //mealPlan.Recipes.Add(recipie);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
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
            var mealPlan = _dbContext
                .Set<MealPlan>()
                .Include(m => m.Recipes)
                .SingleOrDefault(m => m.Id == mealPlanId);

            if (mealPlan == null)
                return NotFound();

            var recipeToRemove = mealPlan.Recipes.SingleOrDefault(r => r.Id.ToString() == recipeId);
            if (recipeToRemove != null)
            {
                mealPlan.Recipes.Remove(recipeToRemove);
                await _dbContext.SaveChangesAsync();
            }

            return Ok();
        }

        /// <summary>
        /// Returns a json array of the recipies included in the meal plan.
        /// </summary>
        /// <param name="mealPlanId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> GetRecipes(Guid mealPlanId)
        {
            // Psudo: 1. Get the meal plan associated recipies, 2. Query spoontacular for the recipe details, 3. return the recpies as a json array
        var mealPlan = _dbContext.Set<MealPlan>().Find(mealPlanId);

        if (mealPlan == null)
            return NotFound();


        // Extract recipe names from the MealPlan
        var recipeIds = mealPlan.Recipes
                .Select(r => r.ApiId)
                .ToList();

      
        var recipesJson = await GetRecipesDetailsAsync(recipeIds);

        return Ok(recipesJson);
    }

    private async Task<IEnumerable<object>> GetRecipesDetailsAsync( List<string> recipeIds)
    {
        var recipesDetails = new List<object>();

        var apiUrl = "https://api.spoonacular.com/recipes/complexSearch";

        using (var client = _httpClientFactory.CreateClient())
        {
            // Create a batch request for all recipe names
            var batchRequest = recipeIds.Select(id => new { id = id }).ToList();

            // Convert the batch request to JSON
            var batchRequestJson = JsonSerializer.Serialize(batchRequest);

            // Send the batch request to Spoonacular
            var content = new StringContent(batchRequestJson, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response into a list of recipe details
                var recipesDetailsResponse = await response.Content.ReadFromJsonAsync<List<object>>();
                recipesDetails.AddRange(recipesDetailsResponse);
            }
        }

        return recipesDetails;
    }

        /// <summary>
        /// Returns a shopping list for the meal plan
        /// </summary>
        /// <param name="mealPlanId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> ShoppingList(Guid mealPlanId)
        {
            var mealPlan = _dbContext.Set<MealPlan>().Find(mealPlanId);

            if (mealPlan == null)
                return NotFound();

            throw new NotImplementedException();
            // var ingredientNames = mealPlan.Recipes.Select(r => r.Ingredients).ToList(); // model needs rework

      
            //var recipesString = await GetIngredientsArrayAsync(ingredientNames);
            // Psudo:  1. Get all the recipies, 2. Collect and consolidate ingredients, 3. Return the array

        }

        private async Task<IEnumerable<string>> GetIngredientsArrayAsync(List<string> ingredientNames)
    {
        var ingredientsArray = new List<string>();

        var apiUrl = "https://api.spoonacular.com/recipes/complexSearch";


        using (var client = _httpClientFactory.CreateClient())
        {
            foreach (var ingredientName in ingredientNames)
            {
                // Include any additional parameters needed for the Spoonacular API
                var parameters = new Dictionary<string, string>
                {
                    { "query", ingredientName }
                };

                var response = await client.GetAsync(apiUrl, HttpCompletionOption.ResponseContentRead);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the JSON response into an object
                    var ingredientDetails = JsonSerializer.Deserialize<object>(await response.Content.ReadAsStringAsync());

                    // Extract relevant information from the Spoonacular response
                    var ingredientInfo = ingredientDetails?.ToString(); // Adjust this based on Spoonacular's response structure

                    ingredientsArray.Add(ingredientInfo);
                }
            }
        }

        return ingredientsArray;
    }

    }
}
