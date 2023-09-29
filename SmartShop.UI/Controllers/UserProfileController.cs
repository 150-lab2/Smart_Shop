using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartShop.UI.Models;
using System.Security.Claims;

namespace SmartShop.UI.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.User = GoogleUser.Create((ClaimsIdentity)User.Identity);

            return View();
        }

    }
}
