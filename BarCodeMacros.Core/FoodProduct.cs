using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BarCodeMacrosUI")]


namespace BarCodeMacros
{
    public class FoodProduct
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required Nutriments NutritionPer100g { get; set; }

        public override string ToString()
        {
            return
                $"Product: {Name}\n" +
                $"Code: {Code}\n" +
                $"Nutrition per 100 g:\n{NutritionPer100g}";
        }
    }
}
