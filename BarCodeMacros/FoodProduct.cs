using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeMacros
{
    class FoodProduct
    {
        public int Id { get; set; }
        public required string Code { get; set; } // PLU code
        public required string Name { get; set; }
        public required Nutriments NutritionPer100g { get; set; }
    }
}
