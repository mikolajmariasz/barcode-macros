using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BarCodeMacrosUI")]


namespace BarCodeMacros
{
    public class Nutriments
    {
        public float? EnergyKcal { get; set; }    
        public float? Proteins { get; set; }     
        public float? Carbohydrates { get; set; } 
        public float? Fat { get; set; }         
        public float? Fiber { get; set; }       
        public float? Salt { get; set; }       

        public Nutriments CalculateForWeight(float weightInGrams)
        {
            return new Nutriments
            {
                EnergyKcal = this.EnergyKcal * weightInGrams / 100,
                Proteins = this.Proteins * weightInGrams / 100,
                Carbohydrates = this.Carbohydrates * weightInGrams / 100,
                Fat = this.Fat * weightInGrams / 100,
                Fiber = this.Fiber * weightInGrams / 100,
                Salt = this.Salt * weightInGrams / 100
            };
        }

        public static Nutriments Sum(IEnumerable<Nutriments> nutriments)
        {
            return new Nutriments
            {
                EnergyKcal = nutriments.Sum(i => i.EnergyKcal),
                Proteins = nutriments.Sum(i => i.Proteins),
                Carbohydrates = nutriments.Sum(i => i.Carbohydrates),
                Fat = nutriments.Sum(i => i.Fat),
                Fiber = nutriments.Sum(i => i.Fiber),
                Salt = nutriments.Sum(i => i.Salt)
            };
        }

        public override string ToString()
        {
            string FormatValue(float? value) => value.HasValue ? value.Value.ToString("0.##") : "N/A";
            return
                $"Energy: {FormatValue(EnergyKcal)}\n" +
                $"Proteins: {FormatValue(Proteins)}\n" +
                $"Carbs: {FormatValue(Carbohydrates)}\n" +
                $"Fat: {FormatValue(Fat)}\n" +
                $"Fiber: {FormatValue(Fiber)}\n" +
                $"Salt: {FormatValue(Salt)}\n";

        }
    }
}
