using System.Windows.Input;
using BarCodeMacros;
using Microsoft.EntityFrameworkCore;

namespace BarCodeMacrosUI
{
    public partial class MainPage : ContentPage
    {
        private MealsContext _db;
        public ICommand DeleteMealCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            _db = new MealsContext();

            DeleteMealCommand = new Command<Meal>(async (meal) =>
            {
                bool result = await DisplayAlert(
                    "Delete meal?",
                    $"Are you sure you want to delete meal '{meal.MealName}'?",
                    "Yes",
                    "No"
                );

                if (result)
                {
                    _db.Meals.Remove(meal);
                    await _db.SaveChangesAsync();
                    LoadMeals();
                }
            });

            LoadMeals();
        }

        private void LoadMeals()
        {
            var meals = _db.Meals
                .Include(m => m.Ingredients)
                .ThenInclude(i => i.Product)
                .ToList();

            BindingContext = new MealsViewModel(meals);
        }

        private async void OnAddMealClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMealPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadMeals();
        }
    }

    public class MealsViewModel
    {
        public List<Meal> Meals { get; set; }
        public bool IsRefreshing { get; set; }
        public Command RefreshCommand { get; }

        public MealsViewModel(List<Meal> meals)
        {
            Meals = meals;
        }
    }
}