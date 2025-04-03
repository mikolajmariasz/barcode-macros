using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BarCodeMacrosUI")]

namespace BarCodeMacros
{
    public class APIService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<FoodProduct?> GetProductByPLUCode(string pluCode) {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://world.openfoodfacts.org/api/v2/product/{pluCode}.json");
                var jsonDoc = JsonDocument.Parse(response);
                if (!jsonDoc.RootElement.TryGetProperty("product", out var productElement)) return null; // checks if there is property 'product' in the response
                //Console.Write(response);
                var product = new FoodProduct {
                    Code = pluCode,
                    Name = productElement.GetProperty("product_name").GetString() ?? "Unknown",
                    NutritionPer100g = new Nutriments()
                };
                if (productElement.TryGetProperty("nutriments", out var nutriments))
                {
                    product.NutritionPer100g = new Nutriments
                    {
                        /*
                         * Checks if property in JSON exists, if yes: returns property as float value, if no: returns null
                         */
                        EnergyKcal = nutriments.TryGetProperty("energy-kcal_100g", out var e) ? e.GetSingle() : null, 
                        Proteins = nutriments.TryGetProperty("proteins_100g", out var p) ? p.GetSingle() : null,
                        Carbohydrates = nutriments.TryGetProperty("carbohydrates_100g", out var c) ? c.GetSingle() : null,
                        Fat = nutriments.TryGetProperty("fat_100g", out var f) ? f.GetSingle() : null,
                        Fiber = nutriments.TryGetProperty("fiber_100g", out var fi) ? fi.GetSingle() : null,
                        Salt = nutriments.TryGetProperty("salt_100g", out var s) ? s.GetSingle() : null
                    };                    
                }
                return product;
            } catch
            {
                return null;
            }
        }
    }
}
