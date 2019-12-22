using NutritionNumerator.Models.DataStore;
using NutritionNumerator.ViewModels;
using System;

using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class AddedFoodPage : ContentPage
    {
        private AddedFoodViewModel viewModel;

        public AddedFoodPage(AddedFoodViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var food = (FoodDS)FoodsListView.SelectedItem;
            await viewModel.DeleteFood(food);
        }
    }
}