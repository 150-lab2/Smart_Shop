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
        public string ApiUri { get; init; } = "";
        public string ApiId { get; init; } = "";
        public string Title { get; init; } = "";
        public string ImageUrl { get; init; } = "";
        public string Description { get; init; } = "";

        public List<ExtendedIngredient> Ingredients = new List<ExtendedIngredient>();

        public static Recipe Create(Guid mealPlanId, string apiId, string apiUri, string title, string description, string imageUrl)
        {
            return new Recipe()
            {
                Id = Guid.NewGuid(),
                ApiUri= apiUri,
                ApiId = apiId,
                Title = title,
                Description = description,
                ImageUrl = imageUrl
            };
        }

        public void AddIngredient(ExtendedIngredient ingredient) => this.Ingredients.Add(ingredient);
    }
}
