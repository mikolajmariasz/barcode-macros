using BarCodeMacros;
using Microsoft.EntityFrameworkCore;

namespace BarCodeMacrosUI
{
    public partial class AddMealPage : ContentPage
    {
        private readonly MealsContext _db;
        private readonly APIService _api;
        private readonly List<Ingredient> _ingredients;

        public AddMealPage()
        {
            InitializeComponent();
            _db = new MealsContext();
            _api = new APIService();
            _ingredients = new List<Ingredient>();
        }

        private async void OnAddIngredientClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PluCodeEntry.Text) ||
                string.IsNullOrWhiteSpace(WeightEntry.Text))
            {
                await DisplayAlert("Błąd", "Wprowadź kod PLU i wagę", "OK");
                return;
            }

            if (!int.TryParse(WeightEntry.Text, out var weight))
            {
                await DisplayAlert("Błąd", "Nieprawidłowa waga", "OK");
                return;
            }

            var product = await _api.GetProductByPLUCode(PluCodeEntry.Text);
            if (product == null)
            {
                await DisplayAlert("Błąd", "Nie znaleziono produktu", "OK");
                return;
            }

            var ingredient = new Ingredient
            {
                Product = product,
                WeightInGrams = weight
            };

            _ingredients.Add(ingredient);
            IngredientsCollection.ItemsSource = null;
            IngredientsCollection.ItemsSource = _ingredients;

            PluCodeEntry.Text = string.Empty;
            WeightEntry.Text = string.Empty;
        }

        private async void OnSaveMealClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MealNameEntry.Text))
            {
                await DisplayAlert("Błąd", "Wprowadź nazwę posiłku", "OK");
                return;
            }

            if (_ingredients.Count == 0)
            {
                await DisplayAlert("Błąd", "Dodaj przynajmniej jeden składnik", "OK");
                return;
            }

            var meal = new Meal
            {
                MealName = MealNameEntry.Text,
                Ingredients = _ingredients
            };

            _db.Meals.Add(meal);
            await _db.SaveChangesAsync();

            await DisplayAlert("Sukces", "Posiłek został zapisany", "OK");
            await Navigation.PopAsync();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}