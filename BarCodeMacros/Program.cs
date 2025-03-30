using BarCodeMacros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BarCodeMacros
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var openFoodFacts = new APIService();
            var db = new MealsContext();

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Dodaj nowy posiłek");
                Console.WriteLine("2. Wyświetl wszystkie posiłki");
                Console.WriteLine("3. Wyjdź");
                Console.Write("Wybierz opcję: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddNewMeal(db, openFoodFacts);
                        break;
                    case "2":
                        DisplayAllMeals(db);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }
            }
        }

        static async Task AddNewMeal(MealsContext db, APIService api)
        {
            Console.Write("Podaj nazwę posiłku: ");
            var mealName = Console.ReadLine();

            var meal = new Meal { MealName = mealName };
            db.Meals.Add(meal);

            while (true)
            {
                Console.Write("Podaj kod PLU składnika (lub 'koniec' aby zakończyć): ");
                var pluCode = Console.ReadLine();

                if (pluCode.ToLower() == "koniec")
                    break;

                Console.Write("Podaj wagę składnika w gramach: ");
                if (!int.TryParse(Console.ReadLine(), out var weight))
                {
                    Console.WriteLine("Nieprawidłowa waga. Spróbuj ponownie.");
                    continue;
                }

                var product = await api.GetProductByPLUCode(pluCode);
                if (product == null)
                {
                    Console.WriteLine("Nie znaleziono produktu o podanym kodzie PLU.");
                    continue;
                }

                Console.WriteLine($"Dodano: {product.Name}");
                meal.Ingredients.Add(new Ingredient { Product = product, WeightInGrams = weight });
            }
            db.SaveChanges();
            Console.WriteLine($"Posiłek '{mealName}' został zapisany.");
        }

        static void DisplayAllMeals(MealsContext db)
        {
            var meals = db.Meals.Include(m => m.Ingredients).ThenInclude(i => i.Product).ToList();

            if (!meals.Any())
            {
                Console.WriteLine("Brak posiłków w bazie danych.");
                return;
            }

            foreach (var meal in meals)
            {
                Console.WriteLine($"\nPosiłek: {meal.MealName}");
                Console.WriteLine("Składniki:");

                foreach (var ingredient in meal.Ingredients)
                {
                    Console.WriteLine($"- {ingredient.Product.Name} ({ingredient.WeightInGrams}g)");
                }

                Console.WriteLine("\nWartości odżywcze (łącznie):");
                Console.WriteLine(meal.TotalNutrition);
            }
        }
    }
}