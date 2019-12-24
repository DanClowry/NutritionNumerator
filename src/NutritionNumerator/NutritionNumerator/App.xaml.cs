using System;
using Xamarin.Forms;
using NutritionNumerator.Views;
using NutritionNumerator.ViewModels;
using NutritionNumerator.Services;
using Xamarin.Essentials;
using System.Threading.Tasks;

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
            settingsService.ToggleDarkMode();

            do
            {
                await ApiKeySetup();
            } while (await ApiKeySetup() == false);

            VersionTracking.Track();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async Task<bool> ApiKeySetup()
        {
            var settingsService = BaseViewModel.Container.Resolve<SettingsService>();
            if (String.IsNullOrWhiteSpace(await settingsService.GetApiKeyAsync()))
            {
                string key = await MainPage.DisplayPromptAsync("No API Key", "An API key is needed to use the app. Please generate one at https://api.data.gov/signup/",
                    cancel: null, placeholder: "API key...");

                if (String.IsNullOrWhiteSpace(key))
                {
                    await MainPage.DisplayAlert("Error Saving API Key", "An API must be set to use the app. It cannot be empty.", "OK");
                    return false;
                }
                try
                {
                    await settingsService.SetApiKeyAsync(key);
                    return true;
                }
                catch (Exception)
                {
                    await MainPage.DisplayAlert("Error Saving API Key", "The API key could not be saved.", "OK");
                    return false;
                }
            }

            return true;
        }
    }
}
