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

        public async Task<IActionResult> Catalogue()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJsonData = await smartShopClient.GetStringAsync($"/api/Recipe/Random?count=10");


            return View("Index", rawJsonData);
        }

        public async Task<IActionResult> Search()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJsonData = await smartShopClient.GetStringAsync($"/api/Recipe/Random?count=10");


            return View("Index", rawJsonData);
        }

        public async Task<IActionResult> ViewRecipe()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJson = await smartShopClient.GetStringAsync($"/api/Recipe/Random?count=10");

            try
            {
                // Read the encoded JSON data from the query parameters
                string rawJsonData = Request.Query["encodedRecipe"];

                // Decode the URL-encoded JSON string
                string decodedJsonData = Uri.UnescapeDataString(rawJsonData);

                // Pass the decoded JSON string to the view
                return View("Index1", decodedJsonData);
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or redirect to an error page
                Console.WriteLine("Error: " + ex.Message);
                return RedirectToAction("Error");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recipe()
        {
            return View();
        }
    }
}
