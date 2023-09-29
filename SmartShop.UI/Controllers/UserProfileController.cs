using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartShop.UI.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
