using Microsoft.AspNetCore.Mvc;
using SmartShop.UI.Models;
using System.Diagnostics;

namespace SmartShop.UI.Controllers
{
    public class Recpie
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new List<Recpie>()
            {
                new Recpie()
                {
                    Name = "Test1",
                    Description = "Really goood meal"
                },
                new Recpie ()
                {
                    Name = "Test 2",
                    Description = "Kinda Gross"
                },
                new Recpie()
                {
                    Name = "Test 3",
                    Description = "Pick Me!"
                }
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}