using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BarCodeMacros
{
    class APIService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<FoodProduct?> GetProductByPLUCode(string pluCode) {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://world.openfoodfacts.org/api/v2/product/{pluCode}.json");
                var jsonDoc = JsonDocument.Parse(response);
            } catch
            {
                return null;
            }
        }
    }
}
