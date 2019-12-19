using NutritionNumerator.ViewModels;
using System;

using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class SummaryPage : ContentPage
    {
        public SummaryPage()
        {
            InitializeComponent();

            BindingContext = new SummaryViewModel();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}