using NutritionNumerator.ViewModels;
using System;

using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class SummaryPage : ContentPage
    {
        private SummaryViewModel viewModel;

        public SummaryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SummaryViewModel();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.GetToday();
        }
    }
}