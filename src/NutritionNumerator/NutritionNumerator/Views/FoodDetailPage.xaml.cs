using NutritionNumerator.ViewModels;
using System;

using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class FoodDetailPage : ContentPage
    {
        private FoodDetailViewModel viewModel;

        public FoodDetailPage(FoodDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await viewModel.SaveFood();
            await Navigation.PopToRootAsync();
        }
    }
}