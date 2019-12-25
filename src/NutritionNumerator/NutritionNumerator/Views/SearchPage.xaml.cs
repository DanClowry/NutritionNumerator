using FoodDataCentral.Models;
using NutritionNumerator.ViewModels;
using Xamarin.Essentials;
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

        async void LoadNextPage(object sender, ItemVisibilityEventArgs e)
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

        async void ItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Failed to Load Food",
                    "An internet connection is needed to load food information. Please enable mobile data or connect to Wi-Fi.", "OK");
                return;
            }

            var food = (SearchResultFood)e.Item;
            await Navigation.PushAsync(new FoodDetailPage(new FoodDetailViewModel(food.FdcId)));
        }
    }
}