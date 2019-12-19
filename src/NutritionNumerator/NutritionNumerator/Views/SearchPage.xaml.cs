using NutritionNumerator.ViewModels;
using System;
using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class SearchPage : ContentPage
    {
        SearchViewModel viewModel;

        public SearchPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SearchViewModel();
        }

        public async void LoadNextPage(object sender, ItemVisibilityEventArgs e)
        {
            if (viewModel.IsBusy || viewModel.Results.Count == 0)
            {
                return;
            }

            if (e.Item == viewModel.Results[viewModel.Results.Count - 1])
            {
                await viewModel.Search(false);
            }
        }
    }
}