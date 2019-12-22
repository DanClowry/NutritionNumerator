using NutritionNumerator.ViewModels;
using System;

using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class SummaryPage : ContentPage
    {
        private SummaryViewModel viewModel;

        public SummaryPage() : this(new SummaryViewModel(DateTime.Today)) { }
        public SummaryPage(SummaryViewModel viewModel)
        {
            InitializeComponent();

            BaseViewModel.Container.Register(viewModel, "selectedViewModel");
            BindingContext = this.viewModel = viewModel;
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.RefreshDay();
        }
    }
}