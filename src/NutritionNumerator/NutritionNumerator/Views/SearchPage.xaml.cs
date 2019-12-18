using NutritionNumerator.ViewModels;

using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();

            BindingContext = new SearchViewModel();
        }
    }
}