using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MealPlan
    {
        public Guid UserId { get; set; }
        public DateTime GeneratedDate { get; set; }

        public List<GroceryItem> ShoppingList = new List<GroceryItem>();

        public void CalculateShoppingList()
        {
            throw new NotImplementedException();
        }
    }
}
