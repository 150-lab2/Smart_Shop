using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Recipe
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public ICollection<GroceryItem> Items { get; init; }

        public static Recipe Create(string name, GroceryItem[] items)
        {
            return new Recipe()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Items = items
            };
        }
    }
}
