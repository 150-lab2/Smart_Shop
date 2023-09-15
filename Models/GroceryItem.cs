using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GroceryItem
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Store { get; init; }
        public string? Department { get; init; }
        public decimal? Price { get; init; }
        public string Size { get; init; }


        // Factory Method to control class creation
        public static GroceryItem Create(string name, string store, string department, decimal price) 
        {
            return new GroceryItem()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Store = store,
                Department = department,
                Price = price
            };
        }
    }
}
