using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class MealPlanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
