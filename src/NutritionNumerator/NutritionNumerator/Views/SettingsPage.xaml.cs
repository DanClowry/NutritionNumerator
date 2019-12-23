using NutritionNumerator.ViewModels;
using System;
using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel viewModel;

        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SettingsViewModel();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                await viewModel.SetApiKeyAsync(ApiKeyField.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to save key", "The API key could not be saved", "OK");
                throw;
            }
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Delete All Data?", 
                "Are you sure you want to delete all data? This action cannot be undone.", 
                "Delete", "Cancel");

            if (response)
            {
                await viewModel.DeleteAllDataAsync();
            }
        }

        async void OnToggle(object sender, EventArgs e)
        {
            await viewModel.SetDarkModeEnabledAsync();
        }
    }
}