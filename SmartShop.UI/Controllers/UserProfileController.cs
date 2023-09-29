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
            var model = GoogleUser.Create(User.Identity);

            return View(model);
        }

    }
}
