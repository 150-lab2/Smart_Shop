using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroceryController : ControllerBase
    {
        private readonly ILogger<GroceryController> _logger;

        public GroceryController(ILogger<GroceryController> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "GetGroceryList")]
        public GroceryItem[] List()
        {
            return new GroceryItem[]
            {
                GroceryItem.Create( "Beans", "WalMart", "Canned Goods", 1.99m),
                GroceryItem.Create( "Tortillas", "WalMart", "Bakery", 2.50m ),
                GroceryItem.Create( "Cheese", "WalMart", "Deli", 3.75m ),
            };
        }
    }


}
