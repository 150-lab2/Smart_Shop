using Microsoft.AspNetCore.Mvc;
using Models;

namespace SmartShop.UI.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RecipeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Suggestion()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJsonData = await smartShopClient.GetStringAsync($"/api/Recipe/Random?count=5");


            return View("Recipe", rawJsonData);
        }
    }
}
