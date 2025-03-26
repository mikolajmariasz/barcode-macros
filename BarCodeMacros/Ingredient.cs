using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeMacros
{
    class Ingredient
    {
        public int Id { get; set; }
        public required FoodProduct Product { get; set; }
        public required int WeightInGrams { get; set; }
        public Nutriments Nutrition => Product.NutritionPer100g.CalculateForWeight(WeightInGrams);
    }
}
