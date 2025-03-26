using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeMacros
{
    class Nutriments
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
                EnergyKcal = this.EnergyKcal * 100 / weightInGrams,
                Proteins = this.Proteins * 100 / weightInGrams,
                Carbohydrates = this.Carbohydrates * 100 / weightInGrams,
                Fat = this.Fat * 100 / weightInGrams,
                Fiber = this.Fiber * 100 / weightInGrams,
                Salt = this.Salt * 100 / weightInGrams
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
            return
                $"Energy: {EnergyKcal}\n" +
                $"Proteins: {Proteins}\n" +
                $"Carbohydrates: {Carbohydrates}\n" +
                $"Fat: {Fat}\n" +
                $"Fiber: {Fiber}\n" +
                $"Salt: {Salt}\n";

        }
    }
}
