using FoodDataCentral;
using FoodDataCentral.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutritionNumerator.ViewModels
{
    class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<SearchResultFood> Results { get; private set; }
        public ICommand SearchCommand { get; }
        public ICommand LoadPageCommand { get; }

        private string searchTerm;
        private int currentPage = 1;
        private int maxPage;

        FoodDataCentralAPI api = Container.Resolve<FoodDataCentralAPI>();

        public SearchViewModel()
        {
            Title = "Search";
            Results = new ObservableCollection<SearchResultFood>();

            SearchCommand = new Command<string>(async (string term) => { searchTerm = term; await Search(true); });
        }

        public async Task Search(bool resetList)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlert("Search Failed",
                    "An internet connection is needed to search for foods. Please enable mobile data or connect to Wi-Fi.", "OK");
                IsBusy = false;
                return;
            }

            if (IsBusy || (maxPage == currentPage && !resetList))
            {
                return;
            }

            IsBusy = true;

            if (resetList)
            {
                currentPage = 1;
                Results.Clear();
            }

            SearchResult result;
            try
            {
                result = await api.Search(searchTerm, pageNumber: currentPage, requireAllWords: true);
                if (result.Foods == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException)
            {
                await Application.Current.MainPage.DisplayAlert("Search Failed", 
                    "Could not load search results. Please check your API key in the settings menu.", "OK");
                IsBusy = false;
                return;
            }
            maxPage = result.TotalPages;

            foreach (var food in result.Foods)
            {
                Results.Add(food);
            }

            if (maxPage != currentPage && !resetList)
            {
                currentPage += 1;
            }

            IsBusy = false;
        }
    }
}
