using Microsoft.AspNetCore.Mvc;

namespace SmartShop.UI.Controllers
{
    public class MealPlanController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MealPlanController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            // we want to access the back end api

            var client = _httpClientFactory.CreateClient("SmartShopClient");


            return View();
        }
    }
}
