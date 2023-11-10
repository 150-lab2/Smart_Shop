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
        public Guid MealPlanId { get; init; }
        public string ApiId { get; init; } = "";
        public string Title { get; init; } = "";

        public static Recipe Create(Guid mealPlanId, string ApiId, string title)
        {
            return new Recipe()
            {
                Id = Guid.NewGuid(),
                ApiId = ApiId,
                Title = title,
            };
        }
    }
}
