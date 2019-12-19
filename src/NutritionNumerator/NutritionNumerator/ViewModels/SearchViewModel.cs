using FoodDataCentral;
using FoodDataCentral.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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

            var result = await api.Search(searchTerm, pageNumber: currentPage, requireAllWords: true);
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
