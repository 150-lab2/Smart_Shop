using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartShop.UI.Models;
using Models;
using System.Security.Claims;

namespace SmartShop.UI.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var googleUser = GoogleUser.Create(User.Identity);
            ViewData["GoogleImageUrl"] = googleUser.Picture;

            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            string encodedEmail = Uri.EscapeDataString(googleUser.Email);


            try
            {
                var userId = await smartShopClient.GetStringAsync($"/api/User/FindUserId?email={encodedEmail}");
                var model = await smartShopClient.GetFromJsonAsync<UserProfile>($"/api/User/GetUser?id={userId.Trim('"')}");
                return View(model);
            } catch (HttpRequestException ex)
            {
                if(ex.StatusCode.HasValue && ex.StatusCode.Value == System.Net.HttpStatusCode.NotFound)
                {
                    // Otherwise the user did not already exist, so we will first build and save the profile before returning.
                    var newUser = new UserProfile()
                    {
                        Id = new Guid(),
                        FirstName = googleUser.GivenName,
                        LastName = googleUser.SurName,
                        EmailAddress = googleUser.Email,
                        AllergiesJSON = "[]",
                        DietTypesJSON = "[]",
                    };

                    var response = await smartShopClient.PostAsJsonAsync<UserProfile>("/api/User/PostUser", newUser);
                    if (response.IsSuccessStatusCode)
                        return View(newUser);
                }
            }
            
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

    }
}
