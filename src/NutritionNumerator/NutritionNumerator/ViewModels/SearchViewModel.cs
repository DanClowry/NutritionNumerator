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

        FoodDataCentralAPI api = Container.Resolve<FoodDataCentralAPI>();

        public SearchViewModel()
        {
            Title = "Search";
            Results = new ObservableCollection<SearchResultFood>();

            SearchCommand = new Command<string>(async (string term) => await Search(term));
        }

        public async Task Search(string searchTerm)
        {
            var result = await api.Search(searchTerm);
            Results.Clear();
            foreach (var food in result.Foods)
            {
                Results.Add(food);
            }
        }
    }
}
