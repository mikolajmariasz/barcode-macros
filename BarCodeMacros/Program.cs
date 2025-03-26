using BarCodeMacros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodAppCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var openFoodFacts = new APIService();
            string pluCode = Console.ReadLine();
            FoodProduct testProduct = await openFoodFacts.GetProductByPLUCode(pluCode);

            Console.WriteLine($"{testProduct}");
            Nutriments nutrimentsIn500g = testProduct.NutritionPer100g.CalculateForWeight(500);
            Console.WriteLine(nutrimentsIn500g);
        }
    }
}
