using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeMacros
{
    class Meal
    {
        public int Id { get; set; }
        public required string MealName { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public Nutriments TotalNutrition => Nutriments.Sum(Ingredients.Select(i => i.Nutrition));
    }
}
