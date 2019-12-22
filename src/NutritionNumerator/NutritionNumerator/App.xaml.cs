using System;
using Xamarin.Forms;
using NutritionNumerator.Views;
using Xamarin.Essentials;
using NutritionNumerator.ViewModels;
using NutritionNumerator.Services;

namespace NutritionNumerator
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected async override void OnStart()
        {
            var settingsService = BaseViewModel.Container.Resolve<SettingsService>();
            if (String.IsNullOrWhiteSpace(await settingsService.GetApiKeyAsync()) || await settingsService.GetApiKeyAsync() == "DEMO-KEY")
            {
                await settingsService.SetApiKeyAsync("DEMO-KEY");
                string key = await MainPage.DisplayPromptAsync("No API Key", "An API key is needed to use the app. Please generate one at https://api.data.gov/signup/");
                try
                {
                    await settingsService.SetApiKeyAsync(key);
                }
                catch (Exception)
                {
                    await MainPage.DisplayAlert("Unable to save key", "The API key could not be saved", "OK");
                    throw;
                }
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
