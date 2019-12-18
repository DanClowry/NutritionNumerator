using NutritionNumerator.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        AboutViewModel viewModel;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AboutViewModel();
            ApiKeyField.Text = Task.Run(async() => await viewModel.GetApiKeyAsync()).Result;
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
    }
}