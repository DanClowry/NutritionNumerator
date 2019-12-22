using NutritionNumerator.Models.DataStore;
using NutritionNumerator.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel viewModel;

        public HistoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HistoryViewModel();
        }

        async void ItemSelected(object sender, ItemTappedEventArgs e)
        {
            var day = (Day)e.Item;
            await Navigation.PushAsync(new SummaryPage(new SummaryViewModel(day.Date)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetDaysCommand.Execute("");
        }
    }
}