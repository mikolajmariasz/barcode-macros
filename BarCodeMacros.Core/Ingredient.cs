using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BarCodeMacrosUI")]


namespace BarCodeMacros
{
    public class Ingredient
    {
        public int Id { get; set; }
        public required FoodProduct Product { get; set; }
        public required int WeightInGrams { get; set; }
        public Nutriments Nutrition => Product.NutritionPer100g.CalculateForWeight(WeightInGrams);

        [NotMapped]
        public string DisplayText => $"{Product?.Name} - {WeightInGrams}g";
    }
}
